using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShortcutPlanetStat : Shortcut
{
    internal Planet planet;
    private WindowPlanetStats windowScript;
    
    protected override void Start()
    {
        appName = planet.planetName + " Stats";
        Icon = planet.shortcut.thumb;
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    protected override void CreateWindow()
    {
        base.CreateWindow();
        windowScript = (WindowPlanetStats)manager.windows[window].script;
        windowScript.planet = planet;
    }
    
}
