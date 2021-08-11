using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mod
{
    public string modName;
    public string description;
    public Sprite icon;

    public Mod(string modName, string description, Sprite icon)
    {
        this.modName = modName;
        this.description = description;
        this.icon = icon;
    }
}
