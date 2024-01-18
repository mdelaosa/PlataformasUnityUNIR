using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Animator anim;
    public AudioSource click;

    // Start is called before the first frame update
    void Start()
    {
        anim.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        anim.SetBool("Encima", true);
        if (Input.GetMouseButtonDown(0))
        {
            click.PlayOneShot(click.clip);
            anim.SetBool("Pulsado", true);
            StartCoroutine(EsperaCambio());
        }
    }

    private void OnMouseExit()
    {
        anim.SetBool("Encima", false);
    }
    IEnumerator EsperaCambio()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Pulsado", false);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

}
