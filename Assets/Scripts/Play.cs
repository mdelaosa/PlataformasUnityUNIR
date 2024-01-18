using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    public Animator anim;
    public int NivelAleatorio;
    public AudioSource click;

    private void Start()
    {
        NivelAleatorio = Random.Range(1, 2);
    }


    //Al estar encima del botón
    private void OnMouseOver()
    {
        anim.SetBool("Encima", true);

        //Al pulsar
        if (Input.GetMouseButtonDown(0))
        {
            click.PlayOneShot(click.clip);
            anim.SetBool("Pulsado", true);
            StartCoroutine(EsperaCambio());
        }
    }

    //Cuando ya no está encima
    private void OnMouseExit()
    {
        anim.SetBool("Encima", false);
    }
    IEnumerator EsperaCambio()
    {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("Pulsado", false);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(NivelAleatorio);
    }

}
