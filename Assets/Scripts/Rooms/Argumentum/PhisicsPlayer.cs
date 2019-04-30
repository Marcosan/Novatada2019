using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhisicsPlayer : MonoBehaviour{

    public float moveForce = 3f;
    public float maxSpeed = 6f;

    private Player player;
    private Vector2 actualCheckpoint;
    
    private void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    
    void FixedUpdate(){
        player.ApplyPhysicsPlatform(moveForce, maxSpeed);
    }

    public void SetCheckpoint(Vector2 checkpoint) {
        actualCheckpoint = checkpoint;
    }

    public Vector2 GetCheckpoint() {
        return actualCheckpoint;
    }
}





