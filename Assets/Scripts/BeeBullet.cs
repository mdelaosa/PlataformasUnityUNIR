using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectile : MonoBehaviour
{
    public float beeTimer;
    public GameObject beeBullet;
    public Transform beeBulletPos;

    void Start()
    {

    }

    void Update()
    {
        beeTimer += Time.deltaTime;

        if(beeTimer > 2)
        {
            beeTimer = 0;
            beeShoot();
        }
    }

    void beeShoot()
    {
        Instantiate(beeBullet, beeBulletPos.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            Destroy(gameObject);
        }

    }
}