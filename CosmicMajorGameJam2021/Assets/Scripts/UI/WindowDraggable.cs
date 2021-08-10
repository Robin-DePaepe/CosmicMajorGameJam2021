using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class WindowDraggable : Window, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private Vector2 oldPos;
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
        oldPos = Input.mousePosition;
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if (pointerDown)
        {

            Vector2 change = mousePos - oldPos; //the change is calculated between the previous and current mouse position

            rect.localPosition += (Vector3)change; //this difference is added to the window's position

            Vector3? correctedPosition = manager.withinScreen(rect);
            
            if (correctedPosition != null) //checks if the corrected position is not null (meaning that it is out of bounds)
            {
                Vector3 position = (Vector3)correctedPosition;
                position.z = 0;
                rect.position = position;
                //sets the window position to the corrected position
                
                pointerDown = false;
                //lifts pointer (to prevent mouse from drifting around and then the window potentially being dragged from out of the window
            }

            oldPos = mousePos;
        }
    }

    public void ConjoinedClicked()
    {
        conjoinedWindow.SetActive(!conjoinedWindow.activeSelf);
    }
    
}
