using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowModStorage : WindowMods
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void Awake()
    {
        base.Awake();
        mods = ModManager.main.mods;
        ModManager.main.modWindow = this;
    }

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < ModManager.main.mods.Count; i++)
        {
            CreateMod(ModManager.main.mods[i]);
        }
    }
    
    public override void CreateMod(Mod mod)
    {
        base.CreateMod(mod);
    }
}
