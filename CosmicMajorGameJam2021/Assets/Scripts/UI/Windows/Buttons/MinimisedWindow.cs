using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinimisedWindow : MonoBehaviour
{
    private Image image;
    internal Window window;
    
    private void Start()
    {
        image = GetComponentInChildren<Image>();
        image.sprite = window.Icon;
        //sets the text of the button to be the name of the window, this can be changed to the windows icon
    }

    private void Update()
    {
        if (window.gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }

    public void OnButtonClick()
    {
        window.gameObject.SetActive(true);
        Destroy(gameObject);
        //re-enables the window and destroys itself
        //due to the horizontal layout group the buttons sort themselves out
    }
}
