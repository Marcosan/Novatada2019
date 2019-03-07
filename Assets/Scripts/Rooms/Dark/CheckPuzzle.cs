using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPuzzle : MonoBehaviour {

    public AntorchaManager antorchaManager;
    public GoalManager goalManager;
    private bool MapClear = false;

    void OnTriggerEnter2D (Collider2D collider) {
        // Si es un ataque o interaccion con el medio
        if (collider.tag == "Action"){
            if (antorchaManager.CheckNumberTorches() && goalManager.CheckLetterClear()) {
                print("Mapa Completo Superado");
                MapClear = true;
            }
        }
    }

    public bool CheckMapClear() {
        return this.MapClear;
    }
}
