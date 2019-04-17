using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour {
    private List<Transform> childrenLetter;
    private List<Transform> childrenCurrent;
    private bool fillChild = true;
    private int totalChild;
    private bool LetterClear = false;

    public Transform glitchNeko;
    public GameObject DarkZones;
    public GameObject SMask_Player;
    public GameObject DarkGrids;
    public GameObject PixelCat;
    public Transform Corchetes;


    void Start(){
        childrenLetter = new List<Transform>();
        childrenCurrent = new List<Transform>();
        foreach (Transform child in transform){
            childrenLetter.Add(child);
        }
        totalChild = childrenLetter.Count;
    }

    void Update(){
        if (!LetterClear) { 
            if (SingletonVars.Instance.GetIsCounting()) {
                if (fillChild) {
                    foreach (Transform child in transform){
                        childrenCurrent.Add(child);
                    }
                    fillChild = false;
                }
                foreach(Transform child in childrenCurrent.ToArray()) {
                    if (child.GetComponent<DrawTrail>().IsActive()){
                        childrenCurrent.Remove(child);
                        //countChild--;
                    }
                }
                if (childrenCurrent.Count == 0){
                    YouWinLetter();
                }
            } else {
                fillChild = true;
            }
        }
    }
    
    private void YouWinLetter() {
        print("Puzzle Letra Superado");
        SingletonVars.Instance.ActiveZoom("out", "far", true);
        SingletonVars.Instance.SetIsCounting(false);
        glitchNeko.GetComponent<GlitchManager>().PlayAudioNeko(false);
        glitchNeko.GetComponent<GlitchManager>().ActiveRandomMaskGlitch(false);

        glitchNeko.parent.GetComponent<NpcChase>().SetSpeedNpc(3f,-1f);
        glitchNeko.parent.GetComponent<NpcChase>().SetChaser(false);

        Destroy(glitchNeko.gameObject);
        Destroy(glitchNeko.parent.GetComponent<DamageManager>());
        Destroy(DarkZones);
        Destroy(SMask_Player);
        Destroy(DarkGrids);

        foreach (Transform child in transform){
            Destroy(child.GetComponent<DrawTrail>());
            Color colorAfter = child.GetComponent<DrawTrail>().colorAfter;
            child.GetComponent<SpriteRenderer>().color = new Color(colorAfter.r, colorAfter.g, colorAfter.b, 1f);;
        }
        foreach (Transform child in Corchetes){
            child.GetComponent<SpriteRenderer>().color = Color.black;
        }
        foreach (Transform child in PixelCat.transform){
            Destroy(child.gameObject);
        }
        //Destroy(GetComponent<GoalManager>());
        LetterClear = true;
    }

    public bool CheckLetterClear() {
        return this.LetterClear;
    }


}