using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    float[] lastMov;
    float[] posicion;
    string LastScene;

    public PlayerData(Player player) {

        posicion = new float[3];
        posicion[0] = player.transform.position.x;
        posicion[1] = player.transform.position.y;
        posicion[2] = player.transform.position.z;

        lastMov = new float[2];
        lastMov[0] = player.getLastMov().x;
        lastMov[1] = player.getLastMov().y;

        LastScene = player.GetLastScene();

    }

    public float GetX() {
        return posicion[0];
    }

    public float GetY()
    {
        return posicion[1];
    }

    public float GetZ()
    {
        return posicion[2];
    }

    public Vector2 GetMovement() {
        return new Vector2(lastMov[0],lastMov[1]);
    }

    public string GetLastScene() {
        return LastScene;
    }

}
