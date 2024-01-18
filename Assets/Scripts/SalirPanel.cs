using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalirPanel : MonoBehaviour
{
    public Animator anim;
    public AudioSource click;
    public GameObject panel;
    public GameObject titulo;
    public GameObject mute;

    // Start is called before the first frame update
    void Start()
    {
        anim.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click.PlayOneShot(click.clip);
            anim.SetBool("Pulsado", true);
            StartCoroutine(EsperaCambio());
        }
    }

    IEnumerator EsperaCambio()
    {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("Pulsado", false);
        yield return new WaitForSeconds(0.25f);
        panel.SetActive(false);
        titulo.SetActive(true);
        mute.SetActive(false);
    }

}
