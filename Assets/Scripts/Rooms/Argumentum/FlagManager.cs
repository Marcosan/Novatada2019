using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {
    
    private Area areaScript;

    void Start(){
        areaScript = GameObject.Find("Area").GetComponent<Area>();
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            StartCoroutine(areaScript.ShowArea("¡Nivel Superado!", 1f));
        }
    }
}
