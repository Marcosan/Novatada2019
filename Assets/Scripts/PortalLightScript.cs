using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLightScript : MonoBehaviour {





    private Animator animacion;
    private void Start()
    {
        animacion = GetComponent<Animator>();
    }
    private void Update()
    {

    }



    void OnTriggerEnter2D(Collider2D collision)
    {

         

        if (collision.gameObject.tag == "Player")
        {

            animacion.SetFloat("Entrada", 1);
            animacion.SetFloat("Salida", 0);
            Debug.Log("Esta entrando!");



        }
        else {

            animacion.SetFloat("Entrada", 0);
        }

    }   

    private void OnTriggerStay2D(Collider2D collision)
              {



        if (collision.gameObject.tag == "Player")
        {

            animacion.SetFloat("Estadia", 1);
            Debug.Log("Permanece!");


        }
        else {

            animacion.SetFloat("Estadia", 0);

        }



              }

              void OnTriggerExit2D(Collider2D collision)
              {


        if (collision.gameObject.tag == "Player")
        {
            
            animacion.SetFloat("Salida", 1);
            animacion.SetFloat("Entrada", 0);
            animacion.SetFloat("Estadia", 0);
            Debug.Log("Se va!!");

        }
        else {

            animacion.SetFloat("Salida", 0);
        }



              }
    
    



}
