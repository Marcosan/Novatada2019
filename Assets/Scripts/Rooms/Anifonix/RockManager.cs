using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour {

    public float rotateSpeed = 0.2f;
    public float gravedad = 0.25f;
    public Transform LimiteSuperior;

    private int rotateDirection;
    private float porcentaje;
    private Rigidbody2D rigidbody;

    void Start(){
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

    private bool GetRandomBoolean(){
        return (Random.value > 0.5);
    }
}
