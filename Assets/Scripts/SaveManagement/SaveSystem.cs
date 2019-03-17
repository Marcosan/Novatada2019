using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    // Diferentes Paths para tantear 

    /* Por defecto inicia desde la raiz del proyecto */
    //private static string path = "./Assets/SaveData/player.sav";

    /* PersistentData es mas recomendado para "Windows Store Apps" o "iOS player", por defecto inicia desde una subcarpeta ubicada en AppData/LocalRow/DefaultCompany/ */
    //private static string path = Application.persistentDataPath + "/player.sav";

    /* Por defecto inicia desde Assets */
    private static string Path = Application.dataPath + "/Scripts/SaveManagement/SaveGame/player.sav";

    // Para cambiar la posicion si es necesario
    public static bool wasLoaded = false;

    public static void savePlayer(Player pyr)
    {

        //Output the Game data path to the console
        Debug.Log("Path : " + Path);

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(Path, FileMode.Create);

        PlayerData data = new PlayerData(pyr);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(Path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No se encontro el archivo de guardado en " + Path);
            return null;
        }
    }

}
