using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowsConjoinedPopUp : WindowConjoined
{

    [Tooltip("Time for pop up to remain on screen")] [SerializeField]
    private float lifeTime;
    
    public TextMeshProUGUI popupUIText;
    public GameObject header;
    public GameObject body;
    public Image image;

    public void SetPop(string text, float timeTillPop, float timeToLast)
    {
        lifeTime = timeToLast;
        popupUIText.text = text;
        SetVisuals(false);
        Invoke(nameof(Pop), timeTillPop);
    }

    void SetVisuals(bool setTo)
    {
        body.SetActive(setTo);
        header.SetActive(setTo);
        image.color = new Color(1, 1, 1, (setTo ? 1f : 0f));
    }
    void Pop()
    {
        SetVisuals(true);
        Destroy(gameObject,lifeTime);
    }
    
    
}
