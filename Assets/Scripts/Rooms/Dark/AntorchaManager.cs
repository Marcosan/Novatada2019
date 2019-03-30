using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntorchaManager : MonoBehaviour {
    private int CountTorch = 0;
    public int NumberTorches;
    
    public void CountUp(){
        CountTorch++;
    }

    public void CountDown(){
        CountTorch--;
    }

    public bool CheckNumberTorches() {
        return NumberTorches == CountTorch;
    }

}
