using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Shortcut : MonoBehaviour, IPointerDownHandler
{
    #region Components
    
    [Header("Components")]
    public WindowManager manager;
    protected Image image;
    private TextMeshProUGUI text;
    protected RectTransform rect;
    
    #endregion

    #region Variables

    public Sprite Icon;
    public GameObject windowTemplate;
    internal GameObject window;
    public string appName;
    Vector3 windowPosition;
    internal bool clickedOnce;
    #endregion

    #region Unity Functions

    protected virtual void Start()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
        

        
        if (Icon != null && appName != null)
        {
            Set();
        }

        if (!manager)
        {
            manager = GameObject.FindWithTag("Canvas").GetComponent<WindowManager>();
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    #endregion

    #region Click Functions

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (clickedOnce)
        {
            if (window)
            {
                if (window.activeSelf)
                {
                    window.transform.SetAsLastSibling();
                }
                else
                {
                    window.SetActive(true);
                }
            }
            else
            {
                CreateWindow();
                if (appName == "Mods")
                {
                    GameManager.main.checkTutorial(tutNames.modOpen);
                }
            }

            clickedOnce = false;
        }
        else
        {
            clickedOnce = true;
            StartCoroutine(clickDuration());
        }

    }

    IEnumerator clickDuration()
    {
        yield return new WaitForSeconds(1);
        clickedOnce = false;
    }

    #endregion

    #region Other Functions

    public void Set()
    {
        image.sprite = Icon;
        text.text = appName;
    }



    protected virtual void CreateWindow()
    {
        if (!windowTemplate) return;

        windowPosition = transform.position + (new Vector3((rect.rect.width/2) * rect.lossyScale.x, -(rect.rect.height/2) * rect.lossyScale.y, 0));
        
        window = manager.CreateWindow(windowPosition,windowTemplate, Icon: Icon,check:false);
        Canvas.ForceUpdateCanvases();
        
        RectTransform windowRect = window.GetComponent<RectTransform>();
        window.transform.position += new Vector3((windowRect.rect.width / 2) * windowRect.lossyScale.x, -(windowRect.rect.height / 2) * windowRect.lossyScale.y);
        manager.CorrectPosition(windowRect);
    }

    #endregion

}
