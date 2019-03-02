using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrail : MonoBehaviour {

    private bool isEnter;
    // Start is called before the first frame update
    void Start(){
        GetComponent<SpriteRenderer>().enabled = false;
        isEnter = false;
    }

    // Update is called once per frame
    void Update() {
        if (isEnter) {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            isEnter = true;
            GetComponent<SpriteRenderer>().enabled = true;
            //trail.transform.position = Vector3.MoveTowards(trail.transform.position, target.position, 20 * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            GetComponent<SpriteRenderer>().enabled = false;
            isEnter = false;
        }
    }
}
