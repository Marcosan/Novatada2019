﻿using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour{
    Vector2 mov;
    Vector2 lastMov;
    Rigidbody2D rb2d;
    public float speed = 4f;
    Animator anim;

    private static PlayerData PlData;
    private static GlobalDataGame GmData;

    string ActiveScene;

    public GameObject initialMap;

    CircleCollider2D actionCollider;
    CircleCollider2D interCollider;

    bool interacting = false;
    private bool isActionButton = false;

    float offsetCeilingX = 0;
    float offsetCeilingY = 0;

    Text tittleMiniMap;

    // Joistick
    public Joystick joystick;
    
    private void Awake(){
        Assert.IsNotNull(initialMap);
    }

    // Start is called before the first frame update
    void Start(){
        
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        actionCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        interCollider = transform.GetChild(1).GetComponent<CircleCollider2D>();

        actionCollider.offset = interCollider.offset;
        actionCollider.radius = interCollider.radius;

        actionCollider.enabled = false;

        //Para cambiar el titulo del minimapa
        SingletonVars.Instance.nameCurrentMap = initialMap.name;
        //tittleMiniMap = GameObject.Find("/Area/MiniMap/TitleMap/TitleText").transform.GetComponent<Text>();
        //tittleMiniMap.text = SingletonVars.Instance.SetNameCurrentMap(initialMap.name);
        //MainCamera es el script, se llama a la funcion SetBound creada allí, se pasa el mapa inicial
        Camera.main.GetComponent<MainCamera>().SetBound(initialMap);

        // Para que la posicion inicial este como se guardo la ultima vez
        if (SaveSystem.wasLoaded)
        {
            // Carga los datos guardados la ultima vez
            PlData = SaveSystem.LoadPlayer();

            setPosition(PlData.GetX(), PlData.GetY(), PlData.GetZ());
            setPlayerDirection(PlData.GetMovement());

            SaveSystem.wasLoaded = false;
        }

        // Implementacion para el audio en el cambio de escena
        print("La escena actual es: " + SceneManager.GetActiveScene().name.ToString());
        SoundManager.ChangeMusic();


    }

    // Update is called once per frame
    void Update(){
        if (isActionButton){
            interacting = true;
        } else{
            interacting = false;
            actionCollider.enabled = false;
        }
        //Movements();
        MoveMentsJoyStick();

        Animations(mov);

        Interact();
        
    }
    
    private void FixedUpdate(){
        MoveCharacter();
    }

    void MoveMentsJoyStick() {
        if ((joystick.Horizontal > .2f || joystick.Horizontal < -.2f) ||
            (joystick.Vertical > .2f || joystick.Vertical < -.2f))
        {
            mov = new Vector2(joystick.Horizontal, joystick.Vertical);
            // Almaneca ultimo movimiento
            lastMov = new Vector2(joystick.Horizontal, joystick.Vertical); 
        }
        else
            mov = Vector2.zero;
    }

    void Movements(){
        //Devuelve 1 o -1 segun la direccion de las flechas, 0 si no se mueve uno de los ejes
        mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Animations(Vector2 direction)
    {
        //Para dejar el la direccion a la que camine
        if (direction != Vector2.zero){ //Si el vector de movimiento es distinto de cero
            anim.SetFloat("moveX", direction.x);
            anim.SetFloat("moveY", direction.y);
            anim.SetBool("walking", true);
        } else{
            anim.SetBool("walking", false);
        }
    }

    void Interact(){
        //mov da 1 o -1, por lo que hay que dividir para 2 para el offset
        if (mov != Vector2.zero){
            offsetCeilingX = 0;
            offsetCeilingY = 0;
            float deltaOffset = .2f;
            if (mov.x < -deltaOffset) offsetCeilingX = ((float)Math.Floor(mov.x)) / 2;
            if (mov.y < -deltaOffset) offsetCeilingY = ((float)Math.Floor(mov.y)) / 2;

            if (mov.x > deltaOffset) offsetCeilingX = ((float)Math.Ceiling(mov.x)) / 2;
            if (mov.y > deltaOffset) offsetCeilingY = ((float)Math.Ceiling(mov.y)) / 2;
            
            actionCollider.offset = new Vector2(offsetCeilingX, offsetCeilingY);
            interCollider.offset = new Vector2(offsetCeilingX, offsetCeilingY);
        }

        if (interacting){
            actionCollider.enabled = true;
        }
    }
    
    public Vector2 getMov(){
        return this.mov;
    }

    public Vector2 getLastMov()
    {
        return this.lastMov;
    }

    public Animator getAnimator()
    {
        return this.anim;
    }

    public void setMov(Vector2 m) {
        this.mov = m;
    }

    // Obtener la ultima escena en la que estuvo el player
    public string GetLastScene() {
        return this.ActiveScene;
    }

    // Cambiar el valor booleano al boton
    // Ir a Assets/Scripts/ActionBtn/Changer.cs para ver implementacion.
    public void setIsActionButton(bool action) {
        this.isActionButton = action;
    }

    // Mover al jugador
    void MoveCharacter(){
        rb2d.MovePosition(rb2d.position + getMov() * speed * Time.deltaTime);
    }

    // Asignar al player la posicion indicada
    public void setPosition(float posX, float posY, float posZ) {

        Vector3 positionPlayer;
        positionPlayer.x = posX;
        positionPlayer.y = posY;
        positionPlayer.z = posZ;

        this.transform.position = positionPlayer;
    }

    // Asignar al player una orientacion (Hacia donde esta mirando)
    public void setPlayerDirection(Vector2 dir) {
        if (dir != Vector2.zero)
        {
            anim.SetFloat("moveX", dir.x);
            anim.SetFloat("moveY", dir.y);
        }

    }

    // Para guardar la partida
    public void SavePlayer() {
        // Asigna a una variable la escena antes de guardar.
        ActiveScene = SceneManager.GetActiveScene().name;

        SaveSystem.SaveGame(this);

        SaveLastScene();

        // Guarda al player con los parametros actuales.
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        /* Por si en algun momento se lo vuelve a utilizar */
        //GetTheData();

        LoadLastScene();

        // Para cargar correctamente las escenas tienen que estar registradas en File>Build Settings
        // desde el editor de unity.

        // Nunca usar, hace que se mezclen escenas y los elementos no se eliminan y se crean unos sobre  otros
        //SceneManager.LoadScene(SaveSystem.LastScene,LoadSceneMode.Additive);

        // Forma correcta de cargar una escena asegurandose de eliminar escenas viejas con sus objetos anteriores
        SceneManager.LoadScene(SaveSystem.LastScene, LoadSceneMode.Single);

        // No estoy seguro si elimina las escenas anteriores y solo hace el cambio
        //SceneManager.LoadScene(SaveSystem.LastScene);

        // Para indicar que se acaba de cargar una escena
        SaveSystem.wasLoaded = true;

        // Para evitar que se noten los cambios de audio bruscamente
        SoundManager.BackgroundMusic.mute = true;

    }

    // No preguntes por que esta esto aqui, el editor me lo dio como solucion y no se por que no coge sin ponerlo en metodo
    // Trata de mandar todos los datos que estaban guardados en el GlobalDataGame en una variable de datos temporales en
    // Save System para evitar que se eliminen o sobreescriban datos que no quieres
    // Por ejemplo, que ya hayas utilizado una variable antes y al guardar como no lo cambiaste se haga nulo
    // >:v Solo hazlo no preguntes
    private static void GetTheData()
    {
        /* Obsoleto, pero se lo dejara como plantilla 
         * Esta Obsoleto nomas el valor que se guarda y carga en GlobalDataGame, sigue en uso para ser utilizado guardando otros 
         * tipos de datos que se requieran
         */
        // Carga los datos guardados la ultima vez
        //GmData = SaveSystem.LoadGameData();
        //SaveSystem.LastScene = GmData.GetLastScene();
    }

    /* Se puede usar una clase llamada PlayerPrefs que guarda en un fichero a parte ciertos datos deseados, pero la desventaja es
     * que solo se aplica con tipos de datos simples como strings, enteros o flotantes.
     * De aqui se puede ver como en python, como si fueran diccionarios. El primer argumento es un key y el segundo un value
     * Por lo menos en los Sets
     * En los Gets el primer argumento sigue siendo un key, pero el segundo (Es opcional) sirve para poner un valor por defecto
     * en caso de no existir uno guardado previamente
     */
    void SaveLastScene() {
        PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);
    }

    void LoadLastScene() {
        SaveSystem.LastScene = PlayerPrefs.GetString("lastScene", SceneManager.GetActiveScene().name);
    }
}
