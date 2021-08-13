using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsConjoinedPopUp : WindowConjoined
{

    [Tooltip("Time for pop up to remain on sreen")] [SerializeField]
    private float lifeTime;
    
    public Text popupUIText;
    public GameObject header;
    public GameObject body;
    public Image image;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public void SetPop(string text, float timeForPopUp)
    {
        
        popupUIText.text = text;
        Destroy(gameObject,timeForPopUp);
        SetVisuals(false);
        Invoke("Pop", timeForPopUp);
        

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
