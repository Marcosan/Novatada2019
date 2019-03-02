using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour {

    [SerializeField] private TimeBar timeBar;
    float fullTime;
    float currentTime;
    float speedTime;
    bool isCountTime;

    private void Start(){
        fullTime = 1f;
        speedTime = .1f;
        isCountTime = false;
        timeBar.SetSize(fullTime);
    }
    private void Update(){
        if (isCountTime) {
            if (currentTime <= 0){
                CancelInvoke("TimeDown");
                isCountTime = false;
            }
            //InvokeRepeating("TimeDown", 2.0f, 0.3f);
        }
    }
    void TimeDown(){
        currentTime -= speedTime;
        timeBar.SetSize(currentTime);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        currentTime = fullTime;
        InvokeRepeating("TimeDown", 2.0f, 0.3f);
        isCountTime = true;
    }
}
