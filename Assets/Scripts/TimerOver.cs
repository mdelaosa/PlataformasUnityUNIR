using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerOver : MonoBehaviour
{
    public TextMeshProUGUI tiempoText;
    public string SceneName;
    public float tiempo;
    private bool en_marcha;
    public static float restante;
    [SerializeField] int min, seg;

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
                StartCoroutine(EsperaCortina());
            }
        }

    }

    IEnumerator EsperaCortina()
    {
        yield return new WaitForSeconds(0f);
        Contador.tiempo = 0;
        SceneManager.LoadScene(SceneName);
    }
}

