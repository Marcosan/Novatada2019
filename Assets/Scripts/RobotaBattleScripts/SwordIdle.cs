using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIdle : MonoBehaviour
{
    public GameObject elAvatar;
    private Animator espadaAnimator;
    private Animator playerAnimator;
    private  static GameObject laEspada;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = elAvatar.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Aqui esta tomando los valores del animator del jugador, y copiandolos en el animator
        //de la espada, de esta forma a pesar de ser Child, su animator (y reskin) son distintos
      //  espadaAnimator.SetFloat("EjeX", playerAnimator.GetFloat("moveX"));
      //  espadaAnimator.SetFloat("EjeY", playerAnimator.GetFloat("moveY"));




        //Las siguientes 4 secuencias de IF's mueven la espada a la posicion requerida
        //Cabe resaltar que se requiere especificar la posicion local y no global
        //Intentar moverlo usando la interfaz de animacion, cancela el movimiento del sprite
        if ((playerAnimator.GetFloat("moveX")) > 0.5f)
        { 
            laEspada.transform.localPosition = new Vector3(0.25f, -0.20f, -2f);
            Quaternion A = Quaternion.Euler(0f, 0f, -45f);
            laEspada.transform.rotation = Quaternion.Slerp(elAvatar.transform.rotation,A,0.5f);
            //laEspada.transform.Rotate(A)
           
        }
        if ((playerAnimator.GetFloat("moveX")) < -0.5f)
        {
            laEspada.transform.localPosition = new Vector3(-0.25f, -0.20f, 2f);
            Quaternion B = Quaternion.Euler(0f, 0f, 45f);
            laEspada.transform.rotation = Quaternion.Slerp(elAvatar.transform.rotation, B, 0.5f);

        }
        if ((playerAnimator.GetFloat("moveY")) > 0.5f)
        {
            laEspada.transform.localPosition = new Vector3(0.5f, -0.40f, 2f);
            Quaternion C = Quaternion.Euler(0f, 0f, 45f);
            laEspada.transform.rotation = Quaternion.Slerp(elAvatar.transform.rotation, C, 0.5f);

        }
        if ((playerAnimator.GetFloat("moveY")) < -0.5f)
        {
            laEspada.transform.localPosition = new Vector3(-0.5f, -0.40f, -2f);
            Quaternion D = Quaternion.Euler(0f, 0f, -45f);
            laEspada.transform.rotation = Quaternion.Slerp(elAvatar.transform.rotation, D, 0.5f);
        }
    }


    public static void NewWeapon(GameObject nuevaEspada) {
        laEspada = nuevaEspada;

    }



}
    

