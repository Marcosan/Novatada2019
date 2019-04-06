using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPuzzle : MonoBehaviour {

    public AntorchaManager antorchaManager;
    public LetterManager goalManager;
    private bool MapClear = false;
    public string MapName;

    void OnTriggerEnter2D (Collider2D collider) {
        // Si es un ataque o interaccion con el medio
        if (collider.tag == "Action"){
            if (antorchaManager.CheckNumberTorches() && goalManager.CheckLetterClear()) {
                print("Mapa Completo Superado");
                switch (MapName){
                    case "MapaK":
                        JsonManager.setMapDarkK(true); 
                        break;
                    case "MapaO":
                        JsonManager.setMapDarkO(true);
                        break;
                    case "MapaA":
                        JsonManager.setMapDarkA(true);
                        break;
                }
                MapClear = true;
            }
        }
    }

    public bool CheckMapClear() {
        return this.MapClear;
    }
}
