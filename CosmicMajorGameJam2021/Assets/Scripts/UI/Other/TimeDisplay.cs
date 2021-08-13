using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void Update()
    {
        string hours = TimeManager.main.currentTime.hours.ToString();
        if (hours.Length < 2)
        {
            hours = "0" + hours;
        }
        
        string minutes = TimeManager.main.currentTime.minutes.ToString();
        if (minutes.Length < 2)
        {
            minutes = "0" + minutes;
        }

        timeText.text = hours + ":" + minutes;
    }
}
