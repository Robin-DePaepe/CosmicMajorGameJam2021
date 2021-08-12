using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShortcutPlanet : Shortcut
{
    private PlanetBehaviour planetBehaviour;
    internal Planet planet;
    private Vector3 offset;
    private CircleCollider2D col;
    private WindowModPlanets windowScript;
    protected override void Start()
    {
        planetBehaviour = GetComponent<PlanetBehaviour>();
        planet = GetComponent<Planet>();
        col = GetComponent<CircleCollider2D>();
        offset = new Vector3(col.radius, 0, 0);
        manager = GameObject.FindWithTag("Canvas").GetComponent<WindowManager>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (window)
        {
            if (window.activeSelf)
            {
                window.SetActive(false);
            }
            else
            {
                window.SetActive(true);
                OffSetPosition();
            }
        }
        else
        {
            CreateWindow();
        }


    }

    private void Update()
    {
        if (window)
        {
            planetBehaviour.CanMove = !window.activeSelf;
        }
    }

    protected override void CreateWindow()
    {
        window = manager.CreateWindow(transform.position + offset, windowTemplate,check:false);
        windowScript = (WindowModPlanets)manager.windows[window].script;
        windowScript.planet = planet;
        OffSetPosition();
    }
    void OffSetPosition()
    {
        Canvas.ForceUpdateCanvases();
        RectTransform windowRect = window.GetComponent<RectTransform>();
        window.transform.position = transform.position + offset+ new Vector3((windowRect.rect.width / 2) * windowRect.lossyScale.x, -(windowRect.rect.height / 2) * windowRect.lossyScale.y);
        manager.CorrectPosition(windowRect);
    }
}
