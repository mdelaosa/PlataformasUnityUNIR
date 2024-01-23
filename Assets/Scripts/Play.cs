using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    public Animator anim;
    public static int NivelAleatorio;
    public AudioSource click;
    int highscore;
    public GameObject contador;
    public static int creadorcontador = 0;


    //Para volver visible el HUD de vidas solo una vez
    //public static int CreadordeVidas = 0;

    //Elegir nivel aleatorio dependiendo de los niveles pasados
    private void Start()
    {
        NivelAleatorio = Random.Range(1, 2);
        PlayerController.contador_vidas = 3;
        //CreadordeVidas = 0;
        PlayerController.InicioMusica = 0;
        //++CreadordeVidas;
        Destroyer.destruir = false;
        ++creadorcontador;
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
            Sandias.sandias = 0;
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

        if (creadorcontador <= 1)
        {
            contador.SetActive(true);
        }
        Contador.en_marcha = true;
        SceneManager.LoadScene(NivelAleatorio);
    }

}
