using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShortcutMod : Shortcut, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private WindowMod windowScript;
    internal WindowMods currentFolderScript;
    internal Mod mod;
    private GameObject mouse;
    private GameObject previousParent;
    private RectTransform previousRect;
    public Image icon;
    private void Awake()
    {
        mouse = GameObject.FindWithTag("Mouse");
    }

    protected override void Start()
    {
        base.Start();
        image = icon;
        Set();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CreateWindow();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(window);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
    }

    protected override void CreateWindow()
    {
        GameManager.main.checkTutorial(tutNames.modFileOpen);

        base.CreateWindow();
        windowScript = (WindowMod)WindowManager.main.windows[window].script;
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
                
                currentFolderScript.mods.Remove(mod);
                toPlaceScript.AddMod(gameObject, mod);
                currentFolderScript = toPlaceScript;
                
                placed = true;
            }
        }

        if (!placed)
        {
            transform.SetParent(previousParent.transform);
        }
    }
}
