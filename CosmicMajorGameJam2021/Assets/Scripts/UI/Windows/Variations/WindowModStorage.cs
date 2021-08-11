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
        mods = ModManager.mods;
    }

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < ModManager.mods.Count; i++)
        {
            CreateMod(ModManager.mods[i]);
        }
    }

    protected override void CreateMod(Mod mod)
    {
        base.CreateMod(mod);
    }
}
