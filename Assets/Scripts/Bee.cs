using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private Animator animatorBee;
    private GameObject beeBullets;
    private Transform beeSting;
    private object direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot ()
    {
        GameObject go = Instantiate(beeBullets, beeSting.position, Quaternion.identity);
        //Vector3 direction = new Vector3(0, transform.localScale.y);

        go.GetComponent<BeeBullet>().Setup(direction);
    }
}
