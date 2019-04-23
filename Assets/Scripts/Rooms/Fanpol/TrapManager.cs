using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour{

    private List<Transform> childrenCurrent;
    private List<Transform> childrenFissures;
    private List<float> columna;
    private bool continuar = true;
    public float speed = 3f;
    public Transform traps;
    public Transform fissures;

    void Start(){
        columna = new List<float>() { 0, -2, -4, -6, -8, -10, -12, -14 };
        childrenCurrent = new List<Transform>();
        childrenFissures = new List<Transform>();
        foreach (Transform child in traps){
            childrenCurrent.Add(child);
        }

        foreach (Transform child in fissures){
            childrenFissures.Add(child);
        }        
    }

    void Update(){
        if (continuar) {
            StartCoroutine(casillaRandom());
        }
        
    }

    private IEnumerator casillaRandom() {
        continuar = false;

        Shuffle(columna);

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < childrenFissures.Count; i++){
            childrenFissures[i].transform.localPosition = new Vector2(childrenFissures[i].transform.localPosition.x, columna[i]);
        }

        yield return new WaitForSeconds(speed);

        for(int i = 0; i < childrenCurrent.Count; i++) {
            childrenCurrent[i].transform.localPosition = new Vector2(childrenCurrent[i].transform.localPosition.x, columna[i]);
        }
        continuar = true;
    }

    private void Shuffle(List<float> columna) {
        var count = columna.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = Random.Range(i, count);
            var tmp = columna[i];
            columna[i] = columna[r];
            columna[r] = tmp;
        }
    }
}
