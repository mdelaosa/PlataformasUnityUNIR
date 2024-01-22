using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform objective;
    Vector3 desface;
    public float suavizado;


    private void Start()
    {
        //desfase

        desface = transform.position - objective.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Seguimiento del player mediante posición y un ligero desfase para darle smoothness

            Vector3 objectivePosicion = objective.position + desface;
            transform.position = Vector3.Lerp(transform.position, objectivePosicion, suavizado * Time.deltaTime);
    }

}
