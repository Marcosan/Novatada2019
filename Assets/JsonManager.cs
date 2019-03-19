using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{

    // Para el uso con JSON
    string filePath;
    string jsonString;

    // Instanciamos un objeto de clase a la que vamos a utilizar
    public static GlobalSettings gsettings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        // Esto para leer el archivo mientras carga el juego
        filePath = SaveSystem.MainPath + "Ajustes.json";
        jsonString = File.ReadAllText(filePath);

        // Se instancia el objeto con los datos del archivo
        gsettings = JsonUtility.FromJson<GlobalSettings>(jsonString);

        // Para imprimir y verificar los valores que estan guardados hasta este momento
        print(gsettings);

        /* Para cambiar valores del json lo que hay que hacer es cambiar los valores del objeto que se instancia
         * y luego mandarlo al archivo mediante el JsonUtility, por ejemplo:
         */
        gsettings.namePlayer = "Marco";

        // De esta forma se los guarda
        jsonString = JsonUtility.ToJson(gsettings);
        File.WriteAllText(filePath, jsonString);

        // Si se hace al objeto y sus variables estaticas se las puede editar desde cualquier parte del programa

    }

}

[System.Serializable]
public class GlobalSettings
{
    // Aqui van los datos declarados exactamente igual a como estan sus keys en el archivo json
    public string namePlayer;
    public string lastMapTag;

    public override string ToString()
    {
        return string.Format(" \"namePlayer\" : {0} , \"lastMapTag\" : {1} ", namePlayer,lastMapTag);
    }

}