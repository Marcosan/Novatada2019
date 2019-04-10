using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {

    public int vida;
    public Transform Player;
    public Transform MaskGameOver;
    public Transform Background;

    private IEnumerator nekoCoroutine;
    //public LetterManager letterManager;
    public Transform PuntoReinicio;
    public Club club;

    private Camera cam;
    private float heightCam;
    private float widthCam;
    private Vector3 scaleCam;

    private void Start(){
        cam = Camera.main;

        MaskGameOver.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
        if (club.Equals("kokoa"))
            nekoCoroutine = GetComponent<NpcChase>().RandomVelocityChase();
    }

    private void Update(){
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            if (vida <= 0){
                GetComponent<NpcChase>().SetSpeedNpc(0f, 10f);
                StartCoroutine(GameOverAnimation());
            }
            else {
                vida--;
                GetComponent<NpcChase>().SetSpeedNpc(0f, 3f);
                Debug.Log("Quedan " + vida + " vidas");
            }
            
        }

    }

    public enum Club {
        kokoa, otros
    }

    IEnumerator GameOverAnimation(){
        Vector2 posicionPlayer = new Vector2(Player.position.x, Player.position.y);
        Vector2 posicionCamera = new Vector2(cam.transform.position.x, cam.transform.position.y);

        //tamaño de camara
        // get the sprite width in world space units
        float worldSpriteWidth = Background.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float worldSpriteHeight = Background.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        // get the screen height & width in world space units
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = (worldScreenHeight / Screen.height) * Screen.width;

        // initialize new scale to the current scale
        Vector3 newScale = Background.localScale;

        // divide screen width by sprite width, set to X axis scale
        newScale.x = worldScreenWidth / worldSpriteWidth;
        newScale.y = worldScreenHeight / worldSpriteHeight;

        // apply scale change
        Background.localScale = newScale;
        /*
        heightCam = 2f * cam.orthographicSize;
        widthCam = heightCam * cam.aspect;
        scaleCam = new Vector3(widthCam, heightCam, 1f);
        Background.transform.localScale = scaleCam;
        */
        Background.gameObject.SetActive(true);
        MaskGameOver.gameObject.SetActive(true);

        Background.position = posicionCamera;
        MaskGameOver.position = posicionPlayer;

        Background.GetComponent<Animator>().Play("game_over_fadeout");

        Debug.Log("newScale " + newScale);
        Debug.Log("Background.localScale " + Background.localScale);
        yield return new WaitForSeconds(3f);
        Debug.Log("termina");
        MaskGameOver.GetComponent<Rigidbody2D>().MovePosition(new Vector2(posicionPlayer.x, posicionPlayer.y - 100));

        yield return new WaitForSeconds(3f);
        Player.transform.position = new Vector3(PuntoReinicio.position.x, PuntoReinicio.position.y, transform.position.z);
        MaskGameOver.position = posicionPlayer;
        
        

    }
}
