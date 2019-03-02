using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    private List<Transform> childrenLetter;
    private List<Transform> childrenCurrent;
    private bool fillChild = true;
    private int totalChild;
    private int countChild;
    private Animator anim;

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
                    countChild--;
                }
            }
            if(countChild <= 0) {
                YouWinLetter();
            }
        } else {
            fillChild = true;
            countChild = totalChild;
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
        print("ganaste");
        SingletonVars.Instance.SetIsCounting(false);
        anim.SetBool("IsOpen", false);

        foreach (Transform child in transform){
            //GameObject duplicate = Instantiate(child.gameObject);
            //Destroy will destroy in the next frame
            //GameObject.Destroy(child.gameObject);
            Destroy(child.GetComponent<DrawTrail>());
            //duplicate.transform.SetParent(this.transform);
        }
        Destroy(GetComponent<GoalManager>());
    }
}
