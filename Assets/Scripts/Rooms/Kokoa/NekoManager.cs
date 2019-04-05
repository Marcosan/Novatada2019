using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoManager : MonoBehaviour
{
    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float speed;
    
    // Variable para guardar la posición inicial
    Vector3 initialPosition;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    Vector3 target;
    RaycastHit2D hit;
    Vector3 dir;

    //Para un patron simple
    public GameObject[] pointsWalk;
    int actualPoint = 0;
    int nextPoint = 1;


    private bool isGoing = true;
    GameObject destiny;

    void Start(){
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        destiny = pointsWalk[1];

        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

        transform.gameObject.SetActive(false);

    }

    void Update(){
        NormalMode();
    }
    
    void NormalMode(){
        //Aquí el npc simplemente se mueve
        //anim.SetBool("walking", true);
        dir = (destiny.transform.position - transform.position).normalized;

        //anim.SetFloat("moveX", dir.x);
        //anim.SetFloat("moveY", dir.y);

        destiny = pointsWalk[nextPoint];
        speed = destiny.transform.GetComponent<SpeedPoint>().speedPoint;
        transform.position = Vector3.MoveTowards(transform.position, destiny.transform.position, speed * Time.deltaTime);
            
        //Si llega al punto destiny cambia el indice del destino y origen
        if (transform.position == destiny.transform.position){
            orderPoint();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            Debug.Log("Te atrapó el Gato");
        }
    }

    void orderPoint(){
        actualPoint++;
        nextPoint++;
        if (nextPoint >= pointsWalk.Length)
        {
            nextPoint = 0;
        }
        if (actualPoint >= pointsWalk.Length)
        {
            actualPoint = 0;
        }
    }

}
