using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class WindowDraggable : Window, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private Vector3 offset;
    public GameObject conjoinedWindow;
    public override void Minimise()
    {
        manager.MinimiseWindow(this);
    }
    
    public override void Close()
    {
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        offset = rect.position - getMouseWorldPos();
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

    protected virtual void Update()
    {
        Vector3 mousePos = getMouseWorldPos();

        if (pointerDown)
        {

            rect.position = mousePos + offset;

            Vector3? correctedPosition = manager.withinScreen(rect);
            
            if (correctedPosition != null) //checks if the corrected position is not null (meaning that it is out of bounds)
            {
                manager.CorrectPosition(rect);
                //sets the window position to the corrected position
                
                pointerDown = false;
                //lifts pointer (to prevent mouse from drifting around and then the window potentially being dragged from out of the window
            }

        }
    }

    Vector3 getMouseWorldPos()
    {
        return manager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    public void ConjoinedClicked()
    {
        conjoinedWindow.SetActive(!conjoinedWindow.activeSelf);
    }
    
}
