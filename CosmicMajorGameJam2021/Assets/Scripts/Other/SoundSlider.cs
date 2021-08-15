using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public bool isSfx;
    public Slider slider;
    
    private void Start()
    {
        if (isSfx)
        {
            slider.value = SoundManager.main.sfxVolume;
        }
        else
        {
            slider.value = SoundManager.main.musicVolume;
        }

        slider.minValue = -80;
        slider.maxValue = 20;
        
        slider.onValueChanged.AddListener(valueChanged);
        valueChanged(slider.value);
    }

    void valueChanged(float value)
    {
        if (isSfx)
        {
            SoundManager.main.SfxMixer.SetFloat("SfxVolume", value);
        }
        else
        {
            SoundManager.main.MusicMixer.SetFloat("MusicVolume", value);
        }
    }
}
