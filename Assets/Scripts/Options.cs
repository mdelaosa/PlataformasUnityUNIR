using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public Animator anim;
    public AudioSource click;
    public GameObject panel;
    public GameObject titulo;
    public GameObject botones;

    // Start is called before the first frame update
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
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("Pulsado", false);
        yield return new WaitForSeconds(0.25f);
        panel.SetActive (true);
        titulo.SetActive(false);
        botones.SetActive(false);

    }
}
