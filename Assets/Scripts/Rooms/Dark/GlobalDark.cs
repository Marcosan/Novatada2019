using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDark : Singleton<GlobalDark> {

    private Dictionary<string, bool> listMapsClear;
    private Dictionary<string, bool> ClearMapK;
    private Dictionary<string, bool> ClearMapO;
    private Dictionary<string, bool> ClearMapA;
    private bool isClearLevel = false;
    private bool isClearK = false;
    private bool isClearO = false;
    private bool isClearA = false;

    protected GlobalDark() {
        listMapsClear = new Dictionary<string, bool>();
        ClearMapK = new Dictionary<string, bool>();
        ClearMapO = new Dictionary<string, bool>();
        ClearMapA = new Dictionary<string, bool>();

        fillMaps();
    }

    private void fillMaps(){
        listMapsClear.Add("MapaK", false);
        listMapsClear.Add("MapaO", false);
        listMapsClear.Add("MapaA", false);

        ClearMapK.Add("Antorchas", false);
        ClearMapK.Add("Letra", false);

        ClearMapO.Add("Antorchas", false);
        ClearMapO.Add("Letra", false);

        ClearMapA.Add("Antorchas", false);
        ClearMapA.Add("Letra", false);
    }

    private bool CheckLevelClear() {
        foreach(KeyValuePair<string, bool> entry in listMapsClear)
            if (!entry.Value)
                return false;
        return true;
    }

    private bool CheckMapClear(string letter) {
        Dictionary<string, bool> TmpDictionary = null;
        switch (letter){
            case "K":
                TmpDictionary = ClearMapK;
                break;
            case "O":
                TmpDictionary = ClearMapO;
                break;
            case "A":
                TmpDictionary = ClearMapA;
                break;
        }
        foreach (KeyValuePair<string, bool> entry in TmpDictionary)
            if (!entry.Value)
                return false;
        return true;
    }

    public void SetPuzzleClear(string letter, string puzzle, bool isClear) {
        switch (letter){
            case "K":
                ClearMapK[puzzle] = isClear;
                isClearK = CheckMapClear(letter);
                if (isClearK)
                    listMapsClear["MapaK"] = true;
                break;
            case "O":
                ClearMapO[puzzle] = isClear;
                isClearO = CheckMapClear(letter);
                if (isClearO)
                    listMapsClear["MapaO"] = true;
                break;
            case "A":
                ClearMapA[puzzle] = isClear;
                isClearA = CheckMapClear(letter);
                if (isClearA)
                    listMapsClear["MapaA"] = true;
                break;
        }
        if (CheckLevelClear()) {
            isClearLevel = true;
        }
    }

    public bool CheckIsLevelClear() {
        return this.isClearLevel;
    }
}
