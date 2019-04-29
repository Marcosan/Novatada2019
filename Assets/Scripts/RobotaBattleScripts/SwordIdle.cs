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
        
        espadaAnimator = GetComponent<Animator>();
        playerAnimator = elAvatar.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        espadaAnimator.SetFloat("EjeX", playerAnimator.GetFloat("moveX"));
        espadaAnimator.SetFloat("EjeY", playerAnimator.GetFloat("moveY"));


        
        if ((espadaAnimator.GetFloat("EjeX")) > 0.5f)
        { 
            laEspada.transform.localPosition = new Vector3(1f, 0f, 0f);
        }

        if ((espadaAnimator.GetFloat("EjeX")) < -0.5f

)
        {
            laEspada.transform.localPosition = new Vector3(-1f, 0f, 0f);
        }
        if ((espadaAnimator.GetFloat("EjeY")) > 0.5f

)
        {
            laEspada.transform.localPosition = new Vector3(0.5f, 0f, 0f);
        }

        if ((espadaAnimator.GetFloat("EjeY")) < -0.5f

)
        {
            laEspada.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
        }



    }

    public static void NewWeapon(GameObject nuevaEspada) {
        laEspada = nuevaEspada;

    }



}
    

