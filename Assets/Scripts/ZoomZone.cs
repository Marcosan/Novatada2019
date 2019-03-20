using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomZone : MonoBehaviour {

    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    float minOrtho = 3.0f;
    float maxOrtho = 5.0f;

    private bool isZoomIn = false;

    // Start is called before the first frame update
    void Start() {
        targetOrtho = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(ZoomCamera());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Zoom") {
            isZoomIn = true;
            StartCoroutine(ZoomCamera());
        }
    }

    IEnumerator ZoomCamera() {
        if(isZoomIn)
            targetOrtho -= 0.1f * smoothSpeed;
        else
            targetOrtho += 0.1f * smoothSpeed;
        targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

        yield return new WaitForSeconds(0.5f);
        /*while (Camera.main.orthographicSize > 4) {
            yield return new WaitForSeconds(0.5f);
            Camera.main.orthographicSize -= 0.1f;
        }*/

    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Zoom") {
            isZoomIn = false;
        }
    }

    /* Efecto zoom lampara con camara
     IEnumerator ZoomCamera() {
        print("trigger");
        targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

        
        while (Camera.main.orthographicSize > 1)
        {
            yield return new WaitForSeconds(1f);
            Camera.main.orthographicSize -= 0.1f;
        }
        

        // Reproducimos la animación de destrucción y esperamos
        yield return new WaitForSeconds(10);
    }
    */
}
