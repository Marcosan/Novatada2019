using UnityEngine;

[System.Serializable]
public class PlayerData
{
    float[] LastMov;
    float[] Posicion;
    string LastScene;

    public PlayerData(Player player) {

        // Guarda la ultima escena en la que estaba al momento de guardar
        LastScene = player.GetLastScene();

        // Guarda la uktima posicion en la que se encontraba
        Posicion = new float[3];
        Posicion[0] = player.transform.position.x;
        Posicion[1] = player.transform.position.y;
        Posicion[2] = player.transform.position.z;

        // Guarda el ultimo vector de direccion del personaje (Hacia donde estaba mirando)
        LastMov = new float[2];
        LastMov[0] = player.getLastMov().x;
        LastMov[1] = player.getLastMov().y;

    }

    public float GetX() {
        return Posicion[0];
    }

    public float GetY()
    {
        return Posicion[1];
    }

    public float GetZ()
    {
        return Posicion[2];
    }

    public Vector2 GetMovement() {
        return new Vector2(LastMov[0],LastMov[1]);
    }

    public string GetLastScene() {
        return LastScene;
    }

}
