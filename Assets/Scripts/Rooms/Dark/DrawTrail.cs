using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrail : MonoBehaviour {

    private bool countOver = false;
    private bool isActive = false;

    void Start(){
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update() {
        if (!SingletonVars.Instance.GetIsCounting()) {
            //counterOver hace que se ejecute una sola vez despues que el conteo termine, desactiva todo
            if (countOver) {
                countOver = false;
                GetComponent<SpriteRenderer>().enabled = false;
                isActive = false;
            }
        } else{
            countOver = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (!SingletonVars.Instance.GetIsCounting()) {
            if (collision.gameObject.tag == "Player"){
                //countOver = true;
                GetComponent<SpriteRenderer>().enabled = true;
                isActive = true;
                //trail.transform.position = Vector3.MoveTowards(trail.transform.position, target.position, 20 * Time.deltaTime);
            }
        } else {
            if (collision.gameObject.tag == "Neko"){
                GetComponent<SpriteRenderer>().enabled = true;
                isActive = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (!SingletonVars.Instance.GetIsCounting()) {
            if (collision.gameObject.tag == "Player"){
                GetComponent<SpriteRenderer>().enabled = false;
                isActive = false;
            }
        }
    }

    public bool IsActive() {
        return this.isActive;
    }
    
}
