using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    Vector2 mov;
    Vector2 lastMov;
    Rigidbody2D rb2d;
    public float speed = 4f;
    Animator anim;

    public GameObject initialMap;

    CircleCollider2D actionCollider;
    CircleCollider2D interCollider;

    bool interacting = false;
    private bool isActionButton = false;

    float offsetCeilingX = 0;
    float offsetCeilingY = 0;

    Text tittleMiniMap;

    //Joistick
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

    public void setIsActionButton(bool action) {
        this.isActionButton = action;
    }

    void MoveCharacter(){
        rb2d.MovePosition(rb2d.position + getMov() * speed * Time.deltaTime);
    }

    public void setPosition(float posX, float posY, float posZ) {

        Vector3 positionPlayer;
        positionPlayer.x = posX;
        positionPlayer.y = posY;
        positionPlayer.z = posZ;

        this.transform.position = positionPlayer;
    }

    public void setPlayerDirection(Vector2 dir) {
        if (dir != Vector2.zero)
        {
            anim.SetFloat("moveX", dir.x);
            anim.SetFloat("moveY", dir.y);
        }

    }

    public void SavePlayer() {
        SaveSystem.savePlayer(this);
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        setPosition(data.GetX(), data.GetY(), data.GetZ());

        setPlayerDirection(data.GetMovement());
        
    }
}
