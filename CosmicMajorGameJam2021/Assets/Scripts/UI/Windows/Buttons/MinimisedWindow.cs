using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinimisedWindow : MonoBehaviour
{
    private TextMeshProUGUI text;
    internal Window window;
    
    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = window.windowName;
        //sets the text of the button to be the name of the window, this can be changed to the windows icon
    }

    public void OnButtonClick()
    {
        window.gameObject.SetActive(true);
        Destroy(gameObject);
        //re-enables the window and destroys itself
        //due to the horizontal layout group the buttons sort themselves out
    }
}
