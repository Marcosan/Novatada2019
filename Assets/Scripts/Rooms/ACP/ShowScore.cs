using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

    public Image color;
    public Image marco;
    public Text contador;
    public Text contadorSombra;

    private void Start(){
        color.enabled = false;
        marco.enabled = false;
        contador.enabled = false;
        contadorSombra.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            color.enabled = true;
            marco.enabled = true;
            contador.enabled = true;
            contadorSombra.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            color.enabled = false;
            marco.enabled = false;
            contador.enabled = false;
            contadorSombra.enabled = false;
        }
        
    }

}
