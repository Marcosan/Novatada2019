using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    private List<Transform> childrenLetter;
    private List<Transform> childrenCurrent;
    private bool fillChild = true;
    private int totalChild;
    private Animator anim;
    private bool LetterClear = false;

    void Start(){
        anim = GameObject.Find("Area/TimeBarUI").GetComponent<Animator>();
        childrenLetter = new List<Transform>();
        childrenCurrent = new List<Transform>();
        foreach (Transform child in transform){
            childrenLetter.Add(child);
        }
        totalChild = childrenLetter.Count;
        print("Total: " + totalChild);
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

    //myObject.transform.Clear(); En caso de instancia de objeto
    /*public static Transform Clear(this Transform transform){
        foreach (Transform child in transform){
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }*/

    private void YouWinLetter() {
        print("Puzzle Letra Superado");
        SingletonVars.Instance.SetIsCounting(false);
        anim.SetBool("IsOpen", false);

        foreach (Transform child in transform){
            Destroy(child.GetComponent<DrawTrail>());
            child.GetComponent<SpriteRenderer>().color = new Color32(73,164,58,255);
        }
        //Destroy(GetComponent<GoalManager>());
        LetterClear = true;
    }

    public bool CheckLetterClear() {
        return this.LetterClear;
    }
}
