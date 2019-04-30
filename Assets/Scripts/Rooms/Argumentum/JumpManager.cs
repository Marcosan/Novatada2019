using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpManager : MonoBehaviour {

    public float jumpForce = 50;

    private void FixedUpdate(){
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Action"){
            //transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            transform.parent.GetComponent<Player>().Jump(jumpForce);
        }
        if (collision.gameObject.tag == "Ground"){
            transform.parent.GetComponent<Player>().SetGrounded(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.gameObject.tag == "Ground"){
            transform.parent.GetComponent<Player>().SetGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Ground"){
            transform.parent.GetComponent<Player>().SetGrounded(false);
        }
    }

}
