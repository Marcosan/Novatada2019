using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGameManager : MonoBehaviour {
    
    private Area areaScript;

    void Start(){
        areaScript = GameObject.Find("Area").GetComponent<Area>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player") {
            DoSomethingForWinner();
        }
    }

    public void DoSomethingForWinner() {
        StartCoroutine(areaScript.ShowArea("¡Nivel Superado!", 1f));
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
