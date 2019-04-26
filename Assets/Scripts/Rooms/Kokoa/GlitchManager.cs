using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchManager : MonoBehaviour{

    public Transform glitchNeko;
    private List<Transform> childrenCurrent;

    private int len_mask;
    private int mask_active;
    private AudioSource audioData;

    // Start is called before the first frame update
    void Awake(){
        audioData = GetComponent<AudioSource>();
        childrenCurrent = new List<Transform>();
        foreach (Transform child in glitchNeko) {
            child.gameObject.SetActive(false);
            childrenCurrent.Add(child);
        }
        len_mask = childrenCurrent.Count;
    }

    public IEnumerator RandomMaskGlitch(){
        float timeSpeed = 1;
        int nveces = 0, min = 5, max = 10;
        while (true){
            nveces = Random.Range(min, max);

            for(int i = min; i <= max; i++){
                mask_active = Random.Range(0, len_mask);
                childrenCurrent[mask_active].gameObject.SetActive(true);
            }
            audioData.Play(0);

            yield return new WaitForSeconds(timeSpeed);
            //childrenCurrent[mask_active].gameObject.SetActive(false);
            foreach (Transform child in childrenCurrent.ToArray()){
                child.gameObject.SetActive(false);
            }
        }
    }

    public void PlayAudioNeko(bool audio) {
        if (audio)
            audioData.Play(0);
        else
            audioData.Stop();
    }

    public void ActiveRandomMaskGlitch(bool isActive) {
        if (isActive)
            StartCoroutine(RandomMaskGlitch());
        else
            StopCoroutine(RandomMaskGlitch());
    }

}
