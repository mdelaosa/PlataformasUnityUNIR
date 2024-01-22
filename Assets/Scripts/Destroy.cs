using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public static bool destruir = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {


    }

}
