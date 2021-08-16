using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowMod : WindowDraggable
{
    internal Mod mod;
    public TextMeshProUGUI description;
    public Image icon;
    protected override void Update()
    {
        base.Update();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Set(Mod modParam)
    {
        mod = modParam;
        
        description.text = mod.description;
    }
    
}
