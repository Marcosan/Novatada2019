using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script para crear un objeto destruible
public class PickWeapon : MonoBehaviour
{

    // Variable para guardar el nombre del estado de destruccion
    public string PickAnim;
    // Variable con los segundos a esperar antes de desactivar la colisión
    public float timeForDisable;
    // Variable con el rango de vision
    public float visionRadius;

    //Objeto a instanciar
    public GameObject NPCname;


    // Animador para controlar la animación
    Animator anim;

    // Variable para guardar al jugador
    GameObject player;

    Vector3 forward;
    RaycastHit2D hit;

    void Start()
    {
        // Para recuperar al player
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Action")
        {

            SoundManager.SetClip("A");
            Debug.Log("RECOGELA");
            GameObject laEspada = Instantiate(NPCname,player.transform);
            laEspada.transform.position = player.transform.position;
            laEspada.GetComponent<SwordIdle>().enabled = true;

            SwordIdle.NewWeapon(laEspada);
        }
    }


    private void OnDestroy()
    {
        changer.DisableButton();
    }

    void Update()
    {

        // "Destruir" el objeto al finalizar la animación de destrucción
        // El estado debe tener el atributo 'loop' a false para no repetirse
        //  AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        //stateInfo.normalizedTime >= 1 porque significa que termino la animacion, de [0,1]
        /*      if (stateInfo.IsName(PickAnim) && stateInfo.normalizedTime >= 1)
              {
                  //MODIFICACION DE DAVID PARA ARMA
                  //Antes de destruir el objeto se crea uno nuevo para el jugador
                 GameObject laEspada = Instantiate(NPCname,player.transform);
                  Vector3 A = new Vector3(0,-0.22f,-1f);
                  Quaternion B = Quaternion.Euler(0f,0f,-45f);
                 // laEspada.transform.position = player.transform.position ;
                 // laEspada.transform.rotation =  Quaternion.Slerp(player.transform.rotation,B,1);

                 // SwordIdle.NewWeapon(laEspada);

                  Destroy(gameObject);
                  // En el futuro podríamos almacenar la instancia y su transform
                  // para crearlos de nuevo después de un tiempo
              } */



    }

}