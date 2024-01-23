using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject fire;
    public float tiempo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EsperaFuego());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EsperaFuego()
    {
        yield return new WaitForSeconds(tiempo);
        fire.SetActive(true);
        StartCoroutine(EsperaFin());
    }
    IEnumerator EsperaFin()
    {
        yield return new WaitForSeconds(tiempo);
        fire.SetActive(false);
        StartCoroutine(EsperaFuego());
    }
}
