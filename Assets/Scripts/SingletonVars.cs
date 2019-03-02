using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SingletonVars : Singleton<SingletonVars>{
    protected SingletonVars() { }

    public string nameCurrentMap;

    public string SetNameCurrentMap(string nameMap) {
        string[] split = Regex.Split(nameMap, @"(?<!^)(?=[A-Z])");
        string newName = "";
        foreach (string word in split){
            newName += " " + word;
        }
        return newName;
    }


}
