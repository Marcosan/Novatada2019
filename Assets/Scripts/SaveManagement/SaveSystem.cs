﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    /* Este espacio es para las variables que se utilizaran para el guardado de datos, los que seran serializados */

    // Diferentes rutas para utilizar

    /* Por defecto inicia desde la raiz del proyecto */
    //private static string MainPath = "./Assets/SaveData/";

    /* PersistentData es mas recomendado para "Windows Store Apps" o "iOS player", por defecto inicia desde una subcarpeta ubicada en AppData/LocalRow/DefaultCompany/ */
    //private static string MainPath = Application.persistentDataPath + "/";

    /* Por defecto inicia desde Assets */
    private static string MainPath = Application.dataPath + "/Scripts/SaveManagement/SaveGame/";

    /* Nombres de los archivos */
    private static string playerDataFile = "player.sav";
    private static string gameDataFile = "game.sav";

    /* Utilizo este espacio para poder guardar temporalmente estados en el sistema de juego, para que sean permanentes deben serializarse */
    public static string LastScene;
    // Para cambiar la posicion si es necesario
    public static bool wasLoaded = false;

    public static void SavePlayer(Player pyr)
    {
        string Path = MainPath + playerDataFile;

        //Output the Game data path to the console
        Debug.Log("Path de los datos guardados del jugador: " + Path);

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(Path, FileMode.Create);

        PlayerData data = new PlayerData(pyr);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    // Agregar los parametros tanto en este metodo como en el objeto data que esta mas abajo
    // Los mismos deben estar en el constructor de GlobalDataGame
    public static void SaveGame(Player pyr)
    {
        string Path = MainPath + gameDataFile;

        //Output the Game data path to the console
        Debug.Log("Path de los datos d=guardados del juego en general: " + Path);

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(Path, FileMode.Create);

        GlobalDataGame data = new GlobalDataGame(pyr);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadPlayer()
    {
        string Path = MainPath + playerDataFile;
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

    public static GlobalDataGame LoadGameData()
    {
        string Path = MainPath + gameDataFile;
        if (File.Exists(Path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path, FileMode.Open);

            GlobalDataGame data = formatter.Deserialize(stream) as GlobalDataGame;
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
