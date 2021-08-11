using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    //base class for all windows
    //handles universal things like components required
    protected RectTransform rect;
    internal WindowManager manager;
    internal Sprite Icon;
    protected virtual void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public virtual void Minimise()
    {
        
    }

    public virtual void Close()
    {
        
    }
}
