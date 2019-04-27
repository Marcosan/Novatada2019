using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public Transform colores;
    public int numeroVeces = 10;
    public int contadorVeces = 0;

    private List<Transform> ChildrenColores;
    private List<float> listaX;
    private List<float> listaY;
    private Vector2 PosColor;
    private int cantColores;
    private Area areaScript;

    void Start(){
        listaX = new List<float>() { 0, 2, 4, 6, 8, 10 };
        listaY = new List<float>() { 0, -2, -4, -6, -8 };
        areaScript = GameObject.Find("Area").GetComponent<Area>();
        ChildrenColores = new List<Transform>();
        foreach (Transform child in colores){
            //child.GetComponent<SpriteRenderer>().enabled = false;
            ChildrenColores.Add(child);
        }
        cantColores = ChildrenColores.Count;
        InvokeRepeating("RandomColor", 0, 3f);

    }

    private void RandomColor(){
        int colorRandom = Random.Range(0, cantColores);

        Shuffle(listaX);
        Shuffle(listaY);

        for(int i=0; i < ChildrenColores.Count; i++) {
            SpriteRenderer color = ChildrenColores[i].GetComponent<SpriteRenderer>();
            PosColor = new Vector2(listaX[i], listaY[i]);

            ChildrenColores[i].transform.localPosition = PosColor;
            color.color = new Color(color.color.r, color.color.g, color.color.b, .5f);
            ChildrenColores[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        ChildrenColores[colorRandom].GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetContadorVeces() {
        contadorVeces++;
        Debug.Log(contadorVeces);
        if (CheckIfWin()) {
            DoSomethingForWinner();
        }
    }

    public void DoSomethingForWinner() {
        StartCoroutine(areaScript.ShowArea("¡Nivel Superado!", 1f));
    }

    public void ResetContadorVeces() {
        contadorVeces = 0;
    }

    private bool CheckIfWin() {
        return contadorVeces >= numeroVeces;
    }

    public bool GetRandomBoolean(){
        return (Random.value > 0.5);
    }

    private void Shuffle(List<float> lista) {
        var count = lista.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = Random.Range(i, count);
            var tmp = lista[i];
            lista[i] = lista[r];
            lista[r] = tmp;
        }
    }
}
