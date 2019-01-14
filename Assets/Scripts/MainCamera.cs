using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour{

    Transform target;
    float tLX, tLY, bRX, bRY; //TopLeftX TopLeftY BottomRightX BottomRightY

    float posX, posY;
    Vector2 velocity;

    private bool isCameraControlling = true;
    //float smoothTime = 0.5f;

    void Awake(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start(){
        //Screen.SetResolution(800, 800, true);
    }

    // Update is called once per frame
    void Update(){
        //print(transform.position.y + ", " + target.position.y);
        //Para dar un efecto suavisado a la camara
        /*posX = Mathf.Round(
            Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime) * 1000) / 1000;
        posY = Mathf.Round(
            Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime) * 1000) / 1000;
            */
        //Mathf.Clamp limitala posicion de target, como minimo tLX (superior izquierda) y maximo bRX (inferior derecha)
        //z no se le pone target porque debe estar a la misma profundidad actual, no al del player
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, tLX, bRX),
            Mathf.Clamp(target.position.y, bRY, tLY),
            transform.position.z);
    }

    public void SetBound (GameObject map){
        //atributos del mapa tiled pasado como parametro map
        Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap>();
        //cantidad de celdas que tiene la camara, por lo general 5
        float cameraSize = Camera.main.orthographicSize;

        tLX = map.transform.position.x + cameraSize;
        tLY = map.transform.position.y - cameraSize;
        bRX = map.transform.position.x + config.NumTilesWide - cameraSize;
        bRY = map.transform.position.y - config.NumTilesHigh + cameraSize;

        //FastMove();
    }

    public void FastMove(){
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
        
    }

    public bool getisCameraControlling() {
        return this.isCameraControlling;
    }

    public void setIsCameraControlling(bool control) {
        this.isCameraControlling = control;
    }
}
