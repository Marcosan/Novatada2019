using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAttack : MonoBehaviour
{


    public float tiempoParaRecargar;
    private bool recargando;
    private GameObject elJugador;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (recargando)
        {
            tiempoParaRecargar -= Time.deltaTime;
            if (tiempoParaRecargar < 0)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                elJugador.SetActive(true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D otro)
    {
        if (otro.gameObject.name == "Player")
        {
            //Destroy(otro.gameObject);

            otro.gameObject.SetActive(false);
            recargando = true;

            elJugador = otro.gameObject;

        }
    }



}
