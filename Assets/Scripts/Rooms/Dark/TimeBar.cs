using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : MonoBehaviour{

    public float speedBar;

    private Transform bar;
    private float fullTime;
    private float currentTime;
    private float speedTime;
    private bool isCountTime;
    private Animator anim;

    private void Start(){
        bar = GameObject.Find("Area/TimeBarUI/Bar").transform;
        anim = GameObject.Find("Area/TimeBarUI").GetComponent<Animator>();
        speedTime = .1f; //para editar el tiempo

        fullTime = 1f; // tiempo al 100%
        speedBar = .01f; //Fluidez de la barra
        isCountTime = false;
        SetSize(fullTime);
    }
    
    private void Update(){
        if (isCountTime) {
            if (currentTime <= 0){
                CancelInvoke("TimeDown");
                isCountTime = false;
                SingletonVars.Instance.SetIsCounting(false);
                GetComponent<BoxCollider2D>().enabled = true;
                anim.SetBool("IsOpen", false);
            }
        }
    }
    
    void TimeDown(){
        currentTime -= speedBar;
        if (currentTime <= 0){
            currentTime = 0;
        }
        SetSize(currentTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Action"){
            currentTime = fullTime;
            InvokeRepeating("TimeDown", 0f, speedTime);
            isCountTime = true;
            SingletonVars.Instance.SetIsCounting(true);
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("IsOpen", true);
        }
    }

    public void SetSize(float sizeNormalized) {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

}
