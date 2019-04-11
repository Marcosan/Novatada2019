using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {

    public int vida;
    private int vidaMenos;
    public Transform Player;
    public Transform MaskGameOver;
    public Transform Background;

    //public LetterManager letterManager;
    public Transform PuntoReinicio;
    public Club club;

    private Camera cam;
    private float heightCam;
    private float widthCam;
    private Vector3 scaleCam;

    private Area areaScript;

    private void Start(){
        cam = Camera.main;
        vidaMenos = vida;
        areaScript = GameObject.Find("Area").GetComponent<Area>();
        MaskGameOver.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            if (vidaMenos <= 0){
                //Mando atras al neko para que no estorbe
                GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                GetComponent<SpriteRenderer>().sortingOrder = -10;
                vidaMenos = vida;
                StartCoroutine(GameOverAnimation());
            }
            else {
                vidaMenos--;
                GetComponent<NpcChase>().SetSpeedNpc(0f, 3f);
                Debug.Log("Quedan " + vidaMenos + " vidas");
            }
            
        }

    }

    public enum Club {
        kokoa, otros
    }

    IEnumerator GameOverAnimation(){
        Vector2 posicionCamera = new Vector2(cam.transform.position.x, cam.transform.position.y);
        SpriteRenderer spritePlayer = Player.GetComponent<SpriteRenderer>();
        BoxCollider2D colliderPlayer = Player.GetComponent<BoxCollider2D>();
        Player playerScript = Player.GetComponent<Player>();
        float hPlayer = spritePlayer.bounds.size.x;
        Vector2 posicionPlayer;

        //Configuracion del personaje
        spritePlayer.sortingLayerName = "GameOverPlayer";
        spritePlayer.sortingOrder = 0;
        playerScript.MovePlayer(false);
        colliderPlayer.enabled = false;

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
        Background.gameObject.SetActive(true);
        Background.position = posicionCamera;
        Background.GetComponent<Animator>().Play("game_over_fadeout");
        StartCoroutine(areaScript.ShowArea("¡Otra vez!",3f));

        yield return new WaitForSeconds(2f);//------------------------------------------------------------------------------------
        Debug.Log("Comienza el portal");
        spritePlayer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask; //.VisibleInsideMask or .None
        transform.position = GetComponent<NpcChase>().pointsWalk[0].transform.position;

        Player.transform.position = new Vector3(PuntoReinicio.position.x, PuntoReinicio.position.y, transform.position.z);
        posicionPlayer = new Vector2(Player.position.x, Player.position.y + hPlayer / 2);
        MaskGameOver.position = posicionPlayer;
        posicionCamera = new Vector2(cam.transform.position.x, cam.transform.position.y);
        Background.position = posicionCamera;

        MaskGameOver.gameObject.SetActive(true);
        MaskGameOver.position = posicionPlayer;
        StartCoroutine(MoveToPosition(MaskGameOver, new Vector2(posicionPlayer.x, posicionPlayer.y - 1.8f), 1f));

        yield return new WaitForSeconds(2f);//------------------------------------------------------------------------------------
        Debug.Log("Reinicio");
        Background.GetComponent<Animator>().Play("game_over_fadein");

        StartCoroutine(MoveToPosition(MaskGameOver, new Vector2(posicionPlayer.x, posicionPlayer.y + 0.2f), 1f));

        yield return new WaitForSeconds(2f);//------------------------------------------------------------------------------------
        //Configuracion del personaje
        spritePlayer.sortingLayerName = "Player";
        playerScript.MovePlayer(true);
        colliderPlayer.enabled = true;
        spritePlayer.maskInteraction = SpriteMaskInteraction.None;

        Background.gameObject.SetActive(false);
        MaskGameOver.gameObject.SetActive(false);
    }

    IEnumerator MoveToPosition(Transform transform, Vector2 position, float timeToMove){
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1){
            t += Time.deltaTime / timeToMove;
            transform.position = Vector2.Lerp(currentPos, position, t);
            yield return null;
        }
        Debug.Log("Fin movimiento");
    }

}
