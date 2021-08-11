using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
 
public class NoDragScroll : MonoBehaviour, IEndDragHandler, IBeginDragHandler  {
 
    ScrollRect EdgesScroll;

    private void Start()
    {
        EdgesScroll = GetComponent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        EdgesScroll.StopMovement();
        EdgesScroll.enabled = false;
    }
 
    public void OnEndDrag(PointerEventData data)
    {
        EdgesScroll.enabled = true;
    }
 
}
