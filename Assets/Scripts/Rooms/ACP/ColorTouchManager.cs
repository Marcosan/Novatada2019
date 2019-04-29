using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTouchManager : MonoBehaviour {

    private SpriteRenderer color;
    private bool isPlayer;

    private void Start(){
        color = GetComponent<SpriteRenderer>();
        color.color = new Color(color.color.r, color.color.g, color.color.b, .5f);
        transform.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player")
            isPlayer = true;
        if (collision.gameObject.tag == "Neko")
            isPlayer = false;

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Neko"){
            color.color = new Color(color.color.r, color.color.g, color.color.b, 1f);
            if(!transform.parent.GetComponent<ColorManager>().CheckIsClear())
                transform.parent.GetComponent<ColorManager>().SetContadorVeces(isPlayer);
            transform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
