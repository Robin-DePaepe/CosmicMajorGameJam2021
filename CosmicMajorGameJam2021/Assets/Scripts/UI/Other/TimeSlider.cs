using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = TimeManager.main.rate*2;
        slider.value = TimeManager.main.rate;
        slider.onValueChanged.AddListener(delegate{ onValueChanged(); });
    }

    void onValueChanged()
    {
        if (slider.value == 0 && !TimeManager.main.timePaused)
        {
            TimeManager.main.timePaused = true;
        }
        else if (slider.value > 0 && TimeManager.main.timePaused)
        {
            TimeManager.main.timePaused = false;
            TimeManager.main.rate = slider.value;
        }
        else
        {
            TimeManager.main.rate = slider.value;
        }
    }

}
