using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMods : WindowDraggable
{
    public GameObject modTemplate;
    public GameObject modParent;
    public List<Mod> mods;
    protected override void Update()
    {
        base.Update();
    }

    protected virtual void Awake()
    {
        mods = new List<Mod>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public virtual void CreateMod(Mod mod)
    {
        GameObject modObject = Instantiate(modTemplate, modParent.transform);
        ShortcutMod shortcut = modObject.GetComponent<ShortcutMod>();
        
        shortcut.mod = mod;
        shortcut.appName = mod.modName;
        shortcut.Icon = mod.icon;
        shortcut.manager = manager;
        shortcut.currentFolderScript = this;
    }

    public virtual void AddMod(GameObject modObject, Mod mod)
    {
        mods.Add(mod);
        modObject.transform.SetAsFirstSibling();
    }
}
