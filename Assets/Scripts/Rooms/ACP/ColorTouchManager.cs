using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTouchManager : MonoBehaviour {

    private SpriteRenderer color;

    private void Start(){
        color = GetComponent<SpriteRenderer>();
        color.color = new Color(color.color.r, color.color.g, color.color.b, .5f);
        transform.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            color.color = new Color(color.color.r, color.color.g, color.color.b, 1f);
            transform.parent.GetComponent<ColorManager>().SetContadorVeces();
            transform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
