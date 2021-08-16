using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowAntiVirus : WindowDraggable
{
    public TextMeshProUGUI availableText;
    public Button button;
    public Button closeButton;
    public Button minimiseButton;
    private bool running;
    
    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(OnClick);
    }

    public override void Minimise()
    {
        base.Minimise();
    }

    public override void Close()
    {
        base.Close();
    }

    protected override void Update()
    {
        base.Update();
        if (!running)
        {
            if (PlanetManager.main.checkMalware())
            {
                availableText.text = "Malware Detected";
                availableText.color = Color.red;

                button.interactable = true;
            }
            else
            {
                availableText.text = "No Malware";
                availableText.color = Color.green;

                button.interactable = false;
            }
        }

    }

    void OnClick()
    {
        StartCoroutine(waitEnable(5f));
    }

    IEnumerator waitEnable(float wait)
    {
        running = true;
        
        availableText.text = "Running...";
        availableText.color = Color.yellow;
        button.interactable = false;
        closeButton.interactable = false;
        minimiseButton.interactable = false;

        yield return new WaitForSeconds(wait);
        PlanetManager.main.clearMalware();
        closeButton.interactable = true;
        minimiseButton.interactable = true;
        running = false;
    }
}
