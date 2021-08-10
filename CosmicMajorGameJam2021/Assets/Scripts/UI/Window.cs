using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    //base class for all windows
    //handles universal things like components required
    protected RectTransform rect;
    public WindowManager manager;
    internal string windowName;
    private void Start()
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
