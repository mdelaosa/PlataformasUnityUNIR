using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectile : MonoBehaviour
{
    public float speed = 5;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            Destroy(gameObject);
        }

    }
}