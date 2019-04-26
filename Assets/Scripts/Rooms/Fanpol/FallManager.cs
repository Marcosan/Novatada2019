using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour{
    public Transform Player;
    public Transform PuntoInicio;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "PlayerNeko"){
            Player.GetComponent<Player>().MoveAlone(PuntoInicio.position, 4f);
        }
    }

}
