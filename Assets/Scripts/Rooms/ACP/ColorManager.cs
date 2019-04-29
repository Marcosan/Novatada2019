using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject ContadorUI;
    private Image ColorCorrecto;
    private Image Marco;
    private Transform ColorNeko;
    private ShowScore showScore;
    private bool LevelClear = false;

    void Start(){
        listaX = new List<float>() { 0, 2, 4, 6, 8, 10 };
        listaY = new List<float>() { 0, -2, -4, -6, -8 };
        areaScript = GameObject.Find("Area").GetComponent<Area>();
        ColorNeko = GameObject.Find("MapaGame/PixelCat").transform;
        ColorCorrecto = GameObject.Find("Area/ColorCorrecto").GetComponent<Image>();
        ContadorUI = GameObject.Find("Area/Contador");
        showScore = GameObject.Find("MapaGame/ShowScore").GetComponent<ShowScore>();
        Marco = GameObject.Find("Area/Marco").GetComponent<Image>();

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
        Color colorUI = ChildrenColores[colorRandom].GetComponent<SpriteRenderer>().color;
        ColorCorrecto.color = new Color(colorUI.r, colorUI.g, colorUI.b, 1f);
        ColorNeko.GetComponent<ColorNeko>().DestinyPoint(ChildrenColores[colorRandom].position);
    }

    public void SetContadorVeces(bool isPlayer) {
        if(isPlayer)
            contadorVeces++;
        else
            contadorVeces--;

        contadorVeces = Mathf.Clamp(contadorVeces, 0, numeroVeces);

        ContadorUI.transform.GetChild(0).GetComponent<Text>().text = contadorVeces + "";
        ContadorUI.transform.GetChild(1).GetComponent<Text>().text = contadorVeces + "";

        if (CheckIfWin()) {
            DoSomethingForWinner();
        }

    }

    public void DoSomethingForWinner() {
        LevelClear = true;
        StartCoroutine(areaScript.ShowArea("¡Nivel Superado!", 1f));

        ColorCorrecto.enabled = false;
        Marco.enabled = false;
        ContadorUI.SetActive(false);
        showScore.enabled = false;
        ColorNeko.GetComponent<ColorNeko>().enabled = false;
        ColorNeko.GetComponent<NpcChase>().enabled = true;
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

    public bool CheckIsClear() {
        return LevelClear;
    }
}
