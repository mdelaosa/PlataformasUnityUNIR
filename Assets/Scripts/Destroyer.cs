using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public static bool destruir = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {

        if (destruir == true)
        {
            Debug.Log("Destruido");
            Destroy(gameObject);
        }
    }

}
