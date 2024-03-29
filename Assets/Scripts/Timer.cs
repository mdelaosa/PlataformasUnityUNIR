using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI tiempoText;
    public string SceneName;
    public float tiempo;
    private bool en_marcha;
    public static float restante;
    [SerializeField] int min, seg;
    public GameObject Musica;
    public GameObject CanvasVidas;
    public GameObject cortinaMuerte;

    void Awake()
    {
        restante = (min * 60) + seg;
        //tiempo = 10f;
        en_marcha = true;
    }
    
     void Update()
    {
        tiempo -= Time.deltaTime;
        tiempoText.text = "" + tiempo.ToString("f0"); 

        if (en_marcha)
        {
            restante -= Time.deltaTime;

            if (restante <= 0)
            {
                en_marcha = false;
                Musica.SetActive(false);
                CanvasVidas.SetActive(false);
                cortinaMuerte.SetActive(true);
                StartCoroutine(EsperaCortina());
            }
        }

    }

    IEnumerator EsperaCortina()
    {
        yield return new WaitForSeconds(0.49f);
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneName);
    }


}
