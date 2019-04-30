using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorNeko : MonoBehaviour {
    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float speed;
    
    // Variable para guardar la posición inicial
    private Vector3 initialPosition;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    private Animator anim;
    private Rigidbody2D rb2d;

    private Vector3 dir;

    //Para un patron simple

    private Vector3 destiny;

    private float timeSpeedChange = 2;
    private bool hasNewSpeed;

    private void Awake(){
        anim = GetComponent<Animator>();
    }

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        
        initialPosition = transform.position;
        destiny = initialPosition;
        //InvokeRepeating("RandomVelocityChase", 2, 1);
    }

    void Update(){
        NormalMode();
    }


    void NormalMode(){
        dir = (destiny - transform.position).normalized;
        anim.SetFloat("moveX", dir.x);
        anim.SetFloat("moveY", dir.y);
    
        transform.position = Vector3.MoveTowards(transform.position, destiny, speed * Time.deltaTime);

        //Si llega al punto destiny cambia el indice del destino y origen
        if (transform.position == destiny){
            //Debug.Log("Llego al punto");
        }
        
    }

    public void DestinyPoint(Vector3 point){
        destiny = point;
    }


    /*  Metodo para cambiar la velocidad del modo chase del npc
     *  Recomendado para estilo persecucion
    */
    public IEnumerator RandomVelocityChase(){
        while (true){
            if (!hasNewSpeed){
                speed = Random.Range(1, 6);
                if (speed >= 5){
                    speed = 6;
                    timeSpeedChange = .1f;
                    anim.SetBool("isAttack", true);
                }
                if (speed >= 1 && speed < 5){
                    timeSpeedChange = 2f;
                    anim.SetBool("isAttack", false);
                }
            }
            hasNewSpeed = false;
            yield return new WaitForSeconds(timeSpeedChange);
        }
    }

    //Si timeSec es negativo, la velocidad se mantiene hasta que se vuelva a llamar a la funcion
    public void SetSpeedNpc(float speed, float timeSec){
        StopCoroutine(RandomVelocityChase());
        timeSpeedChange = timeSec;

        this.speed = speed;
        if (timeSec >= 0){
            hasNewSpeed = true;
            StartCoroutine(RandomVelocityChase());
        }
    }
}
