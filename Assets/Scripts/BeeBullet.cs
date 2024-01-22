using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBullet : MonoBehaviour
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

        if(beeTimer > 5)
        {
            beeTimer = 0;
            beeShoot();
        }
    }

    void beeShoot()
    {
        Instantiate(beeBullet, beeBulletPos.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            Destroy(gameObject);
        }

    }

    internal void Setup(object direction)
    {
        throw new NotImplementedException();
    }
}