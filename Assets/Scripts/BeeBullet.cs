using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    public float beeTimer;
    public GameObject beeBullet;
    public Transform beeBulletPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        beeTimer += Time.deltaTime;

        if (beeTimer > 1)
        {
            beeTimer = 0;
            beeShoot();
        }
    }
    void beeShoot()
    {
        Instantiate(beeBullet, beeBulletPos.position, Quaternion.identity);
    }

}