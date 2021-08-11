using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowModPlanets : WindowMods
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Minimise()
    {
        base.Minimise();
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    protected override void CreateMod(Mod mod)
    {
        base.CreateMod(mod);
    }
}
