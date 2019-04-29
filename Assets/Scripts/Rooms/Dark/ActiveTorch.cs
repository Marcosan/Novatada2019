using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTorch : MonoBehaviour {

    public string fireOnState;
    public string fireOffState;
    Animator anim;
    Transform lightEffect;
    public bool isFireOn;

    void Start(){
        anim = GetComponent<Animator>();
        lightEffect = transform.GetChild(0);
        lightEffect.gameObject.SetActive(false);
        SwitchTorch();
    }

    void OnTriggerEnter2D (Collider2D collider) {
        // Si es un ataque o interaccion con el medio
        if (collider.tag == "Action"){
            SwitchTorch();
        }
    }

    protected bool IsFireOn() {
        return this.isFireOn;
    }

    private void SwitchTorch() {
        if (isFireOn) {
            //Apago la antorcha
            anim.SetBool("isFireOn", false);
            isFireOn = false;
            lightEffect.gameObject.SetActive(false);
            //transform.parent.GetComponent<AntorchaManager>().CountDown();
        }
        else {
            //Encendio la antorcha
            anim.SetBool("isFireOn", true);
            isFireOn = true;
            lightEffect.gameObject.SetActive(true);
            //transform.parent.GetComponent<AntorchaManager>().CountUp();
        }
    }
}
