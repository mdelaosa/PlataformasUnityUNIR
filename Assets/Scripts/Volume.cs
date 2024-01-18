using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider;
    public float SliderValue;
    public GameObject mute;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("VolumenAudio", 1f);
        AudioListener.volume = slider.value;
        RevisarMute();
    }

    // Update is called once per frame

    private void Update()
    {
        if (SliderValue == 0)
        {
            mute.SetActive(true);
        }
        else
        {
            mute.SetActive(false);
        }
    }
    public void ChangeSlider(float valor)
    {
        SliderValue = valor;
        PlayerPrefs.SetFloat("VolumenAudio", SliderValue);
        AudioListener.volume = slider.value;
        RevisarMute();
    }

    public void RevisarMute()
    {
        if (SliderValue == 0)
        {
            mute.SetActive(true);
        }
        else
        {
            mute.SetActive(false);
        }
    }
}
