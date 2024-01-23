using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contador : MonoBehaviour
{
    public static float tiempo = 0;
    public static bool en_marcha = true;
    public static float inicio;
    [SerializeField] int min, seg;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        inicio = (min * 60) + seg;
        //tiempo = 10f;
        en_marcha = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (en_marcha == true)
        {
            tiempo += Time.deltaTime;
        }
    }
}
