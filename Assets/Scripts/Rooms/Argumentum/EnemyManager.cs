using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private Player player;

    private void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            Vector2 checkpoint = transform.parent.transform.parent.GetComponent<PhisicsPlayer>().GetCheckpoint();
            player.MoveAlone(checkpoint, 8f);
        }
    }
}
