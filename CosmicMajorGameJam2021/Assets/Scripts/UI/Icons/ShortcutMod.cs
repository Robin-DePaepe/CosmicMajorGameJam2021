using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShortcutMod : Shortcut, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private WindowMod windowScript;
    internal WindowMods currentFolderScript;
    internal Mod mod;
    private GameObject mouse;
    private GameObject previousParent;
    private RectTransform previousRect;
    private void Awake()
    {
        mouse = GameObject.FindWithTag("Mouse");
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void CreateWindow()
    {
        base.CreateWindow();
        windowScript = window.GetComponent<WindowMod>();
        windowScript.Set(mod);
    }


    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        clickedOnce = false;
        previousParent = transform.parent.gameObject;
        previousRect = previousParent.GetComponent<RectTransform>();
        
        transform.SetParent(mouse.transform, false);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool placed = false;
        for (int i = 0; i < manager.windows.Count; i++)
        {
            WindowEntry entryWindow = manager.windows.Values.ToList()[i];
            GameObject windowObj = manager.windows.Keys.ToList()[i];

            if (windowObj == null || !windowObj.activeSelf)
            {
                continue;
            }
            
            if (rect.Overlaps(entryWindow.rect) && entryWindow.script is WindowMods && !placed)
            {
                WindowMods toPlaceScript = (WindowMods)entryWindow.script;
                
                transform.SetParent(toPlaceScript.modParent.transform);
                transform.SetAsFirstSibling();
                
                currentFolderScript.mods.Remove(mod);
                currentFolderScript = toPlaceScript;
                toPlaceScript.mods.Add(mod);
                
                placed = true;
            }
        }

        if (!placed)
        {
            transform.SetParent(previousParent.transform);
        }
    }
}
