using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    //variables que vamos a utilizar en diferentes funciones por eso son globales y objetos que vamos a recuperar
    GameObject player;
    Rigidbody2D rb2d;
    Vector3 target,dir;
    RaycastHit2D hit;
    Vector3 forward;
    float distance;
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();


        //si no es nulo el player entonces ese será nuestro objetivo y se crea el vector con esa distancia 
        if (player != null) {
            target = player.transform.position;
            dir = (target - transform.position).normalized;
        }


    }

    


    private void FixedUpdate()
    {
        //si ya se obtuvo un target movemos nuestro proyectil a esa position 
        if (target != Vector3.zero) { 

        rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);

        }
    }


    //recibe el objeto con el que vamos a collisionar
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //en caso que nuestro collider detecte el player o a cualquier cosa stestrimos nuestro gameObject

        if (collision.transform.tag == "Player" || collision.transform.tag == "Attack") {
            Destroy(gameObject);
        }
    }

   
}
