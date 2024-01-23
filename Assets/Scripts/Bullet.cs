using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            anim.SetBool("Suelo", true);
            StartCoroutine(EsperaExplosion());
        }

    }

    IEnumerator EsperaExplosion()
    {
        yield return new WaitForSeconds(0.18f);
        Destroy(gameObject);
    }
}
