using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActivePlatform : MonoBehaviour {

    public GameObject walls;
    public TilemapRenderer tilemapRenderer;
    public bool isActivator;

    private void Start() {
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (Transform child in walls.transform) {
            child.GetComponent<SpriteRenderer>().enabled = false;
        }
        //print(JsonManager.gsettings);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player") {
            if (isActivator) {
                foreach (Transform child in walls.transform) {
                    child.GetComponent<BoxCollider2D>().enabled = true;
                }
                tilemapRenderer.sortingLayerName = "Default";
            } else {
                foreach (Transform child in walls.transform) {
                    child.GetComponent<BoxCollider2D>().enabled = false;
                }
                tilemapRenderer.sortingLayerName = "Ceilings";
            }
        }
    }
}
