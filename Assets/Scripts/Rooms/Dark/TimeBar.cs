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

    private void Start(){
        bar = GameObject.Find("Area/TimeBarUI/Bar").transform;
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
        if (collision.gameObject.tag == "Player"){
            currentTime = fullTime;
            InvokeRepeating("TimeDown", 0f, speedTime);
            isCountTime = true;
        }
    }

    public void SetSize(float sizeNormalized) {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

}
