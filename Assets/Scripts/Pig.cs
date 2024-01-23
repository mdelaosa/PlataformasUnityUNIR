using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{ 
public Rigidbody2D rb2d;
public float velocidad;
bool derecha = false;
bool activacion = true;

// Start is called before the first frame update
void Start()
{
    rb2d.gameObject.GetComponent<Rigidbody2D>();
}

// Update is called once per frame
void Update()
{
    if (derecha == false)
    {
        rb2d.velocity = new Vector2(-velocidad, 0);
    }
    else
    {
        rb2d.velocity = new Vector2(velocidad, 0);
    }
}
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Trigger" && derecha == false && activacion == true)
    {
        StartCoroutine(EsperaCambio());
        derecha = true;
        activacion = false;
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    if (collision.gameObject.tag == "Trigger" && derecha == true && activacion == true)
    {
        StartCoroutine(EsperaCambio());
        derecha = false;
        activacion = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}


IEnumerator EsperaCambio()
{
    yield return new WaitForSeconds(0.25f);
    activacion = true;
}
}
