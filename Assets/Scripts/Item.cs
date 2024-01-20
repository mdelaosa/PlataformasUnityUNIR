using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Animator anim;
    public GameObject destruible;
    public float tiempo;
    public float impulsoMAX;
    public Rigidbody2D player;

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
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Tocado", true);
            player.AddForce(Vector2.up * impulsoMAX, ForceMode2D.Impulse);
            StartCoroutine(EsperaItem(collision));
        }
    }

    IEnumerator EsperaItem(Collision2D collision)
    {
        yield return new WaitForSeconds(tiempo);
        destruible.SetActive(false);
    }
}
