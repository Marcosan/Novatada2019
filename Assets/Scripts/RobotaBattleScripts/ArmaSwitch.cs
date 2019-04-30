using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaSwitch : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            // En este punto se activa el boton de Accion
            changer.StartAction();

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            // En este punto se activa el boton de Accion
            changer.StartAction();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            // En este punto se desabilita el boton de Accion
            changer.DisableButton();
        }

    }









}
