using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour {

    public float rotateSpeed = 0.2f;
    public float gravedad = 0.25f;
    public Transform LimiteSuperior;

    private Transform retroceso;
    private int rotateDirection;
    private float porcentaje;
    private Rigidbody2D rigidbody;
    private Player PlayerScript;

    void Start(){
        PlayerScript = GameObject.Find("Player").GetComponent<Player>();
        retroceso = GameObject.Find("Player/Retroceso").transform;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update(){
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "LimiteInferior"){
            RandomDirectionRotate();
            RandomGravity();
            MoveRocRandomPositionX();
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            PlayerDamage();
        }
    }

    private void MoveRocRandomPositionX() {
        float random = Random.Range(0, 16);
        Vector3 toLocalPos = transform.parent.InverseTransformPoint(LimiteSuperior.position);
        transform.localPosition = new Vector3(random, toLocalPos.y, transform.position.z);
    }

    private void RandomDirectionRotate() {
        if (GetRandomBoolean())
            rotateDirection = 1;
        else
            rotateDirection = -1;

        rigidbody.AddTorque(rotateDirection * rotateSpeed);
    }

    private void RandomGravity() {
        porcentaje = Random.Range(1, 16);
        porcentaje = porcentaje / 10;
        rigidbody.gravityScale = porcentaje * gravedad;
    }

    private void PlayerDamage() {
        PlayerScript.MoveAlone(retroceso.position, 8f);
    }

    private bool GetRandomBoolean(){
        return (Random.value > 0.5);
    }
}
