using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcChase : MonoBehaviour {

    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float visionRadius;
    public float attackRadius;
    public float speed;

    // Variable para guardar al jugador
    GameObject player;

    // Variable para guardar la posición inicial
    Vector3 initialPosition;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    Vector3 target;
    RaycastHit2D hit;
    Vector3 forward;
    float distance;
    Vector3 dir;

    //Para un patron simple
    public GameObject[] pointsWalk;
    int actualPoint = 0;
    int nextPoint = 1;

    //public float npcSpeed;
    bool isChasing = false;
    public bool isChaser = false;

    private bool isGoing = true;
    GameObject destiny;

    void Start () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        destiny = pointsWalk[1];
        
        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

    }

    void Update () {
        LecturaRayCast();
        if (isChasing && isChaser){
            ChaseMode();
        } else{
            NormalMode();
        }

    }

    // Podemos dibujar el radio de visión y ataque sobre la escena dibujando una esfera
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void NormalMode(){
        //Aquí el npc simplemente se mueve
        //anim.SetBool("walking", true);
        dir = (destiny.transform.position - transform.position).normalized;
        
        anim.SetFloat("moveX", dir.x);
        anim.SetFloat("moveY", dir.y);

        if (isGoing){
            destiny = pointsWalk[nextPoint];
            distance = Vector3.Distance(target, transform.position);
            
            // Si es el enemigo y está en rango corto el npc se detiene
            if (target != initialPosition && distance < attackRadius){
                anim.Play("NpcWalk", -1, 0);  // Congela la animación de andar, velocidad de animacion 0
            } else {
                anim.SetBool("walking", true);
                //MoveTowards: mover hacia un punto
                transform.position = Vector3.MoveTowards(transform.position, destiny.transform.position, speed * Time.deltaTime);
            }

            //Si llega al punto destiny cambia el indice del destino y origen
            if (transform.position == destiny.transform.position){
                orderPoint();
            }
        }
    }

    void orderPoint() {
        actualPoint++;
        nextPoint++;
        if (nextPoint >= pointsWalk.Length) {
            nextPoint = 0;
        }
        if (actualPoint >= pointsWalk.Length) {
            actualPoint = 0;
        }
    }

    void ChaseMode(){
        // Calculamos la distancia y dirección actual hasta el target
        distance = Vector3.Distance(target, transform.position);
        dir = (target - transform.position).normalized;

        // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
        if (target != initialPosition && distance < attackRadius) {
            // Aquí le atacaríamos, pero por ahora simplemente cambiamos la animación
            anim.SetFloat("moveX", dir.x);
            anim.SetFloat("moveY", dir.y);
            anim.Play("NpcWalk", -1, 0);  // Congela la animación de andar, velocidad de animacion 0
        }
        // En caso contrario nos movemos hacia él
        else {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

            // Al movernos establecemos la animación de movimiento, velocidad de animacion se vuelve a 1
            anim.speed = 1;
            anim.SetFloat("moveX", dir.x);
            anim.SetFloat("moveY", dir.y);
            anim.SetBool("walking", true);
        }

        // Una última comprobación para evitar bugs forzando la posición inicial
        if (target == initialPosition && distance < 0.02f) {
            transform.position = initialPosition;
            // Y cambiamos la animación de nuevo a Idle
            anim.SetBool("walking", false);
        }

        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);
    }

    void LecturaRayCast(){
        // Por defecto nuestro target siempre será nuestra posición inicial
        target = initialPosition;

        // Comprobamos un Raycast del enemigo hasta el jugador
        hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            visionRadius,
            1 << LayerMask.NameToLayer("Default")
        // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
        // También poner al objeto Attack y al Prefab Slash una Layer Attack 
        // Sino los detectará como entorno y se mueve atrás al hacer ataques
        );

        // Aquí podemos debugear el Raycast
        forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null) {
            if (hit.collider.tag == "Player") {
                target = player.transform.position;
                //Se convierte en perseguidor
                isChasing = true;
            }
        }
        else {
            isChasing = false;
        }
    }

}