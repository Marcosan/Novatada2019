using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {
        
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            Debug.Log("Pasó por checkpoint");
            transform.parent.transform.parent.GetComponent<PhisicsPlayer>().SetCheckpoint(transform.position);
        }
    }
}
