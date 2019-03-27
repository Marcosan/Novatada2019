using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActivation : MonoBehaviour
{





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

        animacion.SetFloat("FuegoTocado", 0);

        if (collision.gameObject.tag=="Jugador"){

            animacion.SetFloat("FuegoTocado", 1);
            Debug.Log("Esta tocandolo!");

        }
 


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animacion.SetFloat("FuegoTocado", 0);

        if (collision.gameObject.tag == "Jugador")
        {

            animacion.SetFloat("FuegoTocado", 0);
            Debug.Log("Se va!!");

        }




    }




}
