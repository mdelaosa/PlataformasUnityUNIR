using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public Animator anim;
    public string Scene_Name;
    public AudioSource click;

    //Al estar encima del bot�n
    private void OnMouseOver()
    {
        anim.SetBool("Encima", true);

        //Al pulsar el bot�n
        if (Input.GetMouseButtonDown(0))
        {
            click.PlayOneShot(click.clip);
            anim.SetBool("Pulsado", true);
            StartCoroutine(EsperaCambio());
        }
    }

    //Cuando ya no se est� encima
    private void OnMouseExit()
    {
        anim.SetBool("Encima", false);
    }
    IEnumerator EsperaCambio()
    {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("Pulsado", false);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(Scene_Name);
    }

}
