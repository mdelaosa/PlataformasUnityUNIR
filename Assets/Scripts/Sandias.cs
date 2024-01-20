using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sandias : MonoBehaviour
{
    public TextMeshProUGUI puntostext;
    public static int sandias = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        puntostext.text = "" + sandias;
    }
}
