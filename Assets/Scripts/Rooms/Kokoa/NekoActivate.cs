using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoActivate : MonoBehaviour {
    
    public Transform Neko;
    public Transform NekoGlitch;
    SpriteRenderer interCollider;
    //private Animator anim;

    private void Start(){
        interCollider = transform.GetChild(0).GetComponent<SpriteRenderer>();
        interCollider.enabled = false;
        Neko.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Action"){
            Neko.gameObject.SetActive(true);
            SingletonVars.Instance.SetIsCounting(true);
            StartCoroutine(Neko.GetComponent<NpcChase>().RandomVelocityChase());
            StartCoroutine(NekoGlitch.GetComponent<GlitchManager>().RandomMaskGlitch());
        }
        if (collision.gameObject.tag == "Interact"){
            interCollider.enabled = true;
            changer.StartAction();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Interact"){
            interCollider.enabled = false;
            changer.DisableButton();
        }
    }

}
