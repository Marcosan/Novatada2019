using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class JoyStickOld : MonoBehaviour{

    Vector3 mousePos;
    Vector2 mousePos2D;
    RaycastHit2D hit;

    //Touch
    private Vector2 pointA;
    private Vector2 pointB;
    Vector2 mov;
    Rigidbody2D rb2d;
    Transform circleIn;

    //Transform circleOut;
    public GameObject player;
    Player playerScript;
    private Vector2 posJoystick;
    private float speed;

    private bool inside = false;

    private void Awake(){
        Assert.IsNotNull(player);
    }

    // Start is called before the first frame update
    void Start(){
        rb2d = player.GetComponent<Rigidbody2D>();
        speed = player.GetComponent<Player>().speed;
        playerScript = player.GetComponent<Player>();

        posJoystick = transform.position - Camera.main.transform.position;
        circleIn = this.gameObject.transform.GetChild(1).gameObject.transform;
    }

    // Update is called once per frame
    void Update(){
        MovementsTouch();
        TouchPoints();
    }

    private void FixedUpdate(){
        if (inside){
            MovementsTouchFixed();
            MoveCharacter();
        }
    }

    private void TouchPoints() {
        if (Input.GetMouseButtonDown(0)){
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos2D = new Vector2(mousePos.x, mousePos.y);

            hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 100f, 1 << LayerMask.NameToLayer("JoyStick"));
            //Si se toco un objeto
            if (hit.collider != null){
                //Debug.Log(hit.collider.gameObject.name);
                //hit.collider.attachedRigidbody.AddForce(Vector2.up);
                if (hit.collider.gameObject.name.Equals("Joystick_out")) {
                    inside = true;
                }else {
                    inside = false;
                }
                if (hit.collider.gameObject.name.Equals("Joystick_action")){
                    player.GetComponent<Player>().setIsActionButton(true);
                }
            } else {
                inside = false;
            }
            
        }

        if (Input.GetMouseButtonUp(0)) {
            player.GetComponent<Player>().setIsActionButton(false);
        }

        if (Input.GetMouseButton(0)) print("");
    }

    void MovementsTouch(){
        //Clic sostenido
        pointA = transform.position;
        transform.position = new Vector2(
            Camera.main.transform.position.x + posJoystick.x,
            Camera.main.transform.position.y + posJoystick.y
        );
        if (Input.GetMouseButton(0)){
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else{
            circleIn.transform.position = pointA;
            pointB = pointA;
        }
    }

    void MovementsTouchFixed(){
        playerScript.setMov(Vector2.ClampMagnitude(pointB - pointA, 1.0f));

        circleIn.transform.position = new Vector2(
            Camera.main.transform.position.x + posJoystick.x + playerScript.getMov().x,
            Camera.main.transform.position.y + posJoystick.y + playerScript.getMov().y);

    }

    void MoveCharacter(){
        rb2d.MovePosition(rb2d.position + playerScript.getMov() * speed * Time.deltaTime);
    }
}
