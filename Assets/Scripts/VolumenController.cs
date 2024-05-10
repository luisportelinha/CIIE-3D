using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumenController : MonoBehaviour
{
    public Slider slider;
    public float sliderVolume;
    public Image imageMute;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        AudioListener.volume = slider.value;
        isMute();
    }

    public void changeSlider(float value)
    {
        sliderVolume = value;
        PlayerPrefs.SetFloat("MusicVolume", sliderVolume);
        AudioListener.volume = slider.value;
        isMute();
    }   
    public void isMute()
    {
        if (slider.value == 0)
        {
            imageMute.enabled = true;
        }
        else
        {
            imageMute.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
