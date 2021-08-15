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
        //if you don't want the popUp to go away put 0 for timeToLast
        lifeTime = timeToLast;
        popupUIText.text = text;
        SetVisuals(false);
        StartCoroutine(waitPop(timeTillPop));
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
        if (lifeTime > 0)
        {
            Destroy(gameObject,lifeTime);
        }
    }

    IEnumerator waitPop(float wait)
    {
        yield return new WaitForSeconds(wait);
        Pop();
    }
}
