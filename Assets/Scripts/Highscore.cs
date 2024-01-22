using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI puntostext;
    public static int highscore;

    // Start is called before the first frame update
    private void Awake()
    {
        highscore = PlayerPrefs.GetInt("record");

        if (Sandias.sandias > highscore)
        {
            highscore = Sandias.sandias;
            PlayerPrefs.SetInt("record", highscore);
        }
    }
    private void Start()
    {
        puntostext.text = "" + highscore;
    }
}
