using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : MonoBehaviour {

    private Transform Player;
    private Player PlayerScript;

    private void Start(){
        Player = GameObject.Find("Player").transform;
        PlayerScript = Player.GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            //Si mov es negativo es porque Player va hacia abajo
            if (PlayerScript.getMov().y > 0){
                PlayerScript.SetSpeed(2f);
            } else{
                PlayerScript.SetSpeed(6f);
            }
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            PlayerScript.ResetToNormalSpeed();
        }
    }
}
