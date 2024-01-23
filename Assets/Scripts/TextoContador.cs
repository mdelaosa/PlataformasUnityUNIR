using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoContador : MonoBehaviour
{
    public TextMeshProUGUI puntostext;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        puntostext.text = "" + Contador.tiempo.ToString("f0") + "´´";
    }
}
