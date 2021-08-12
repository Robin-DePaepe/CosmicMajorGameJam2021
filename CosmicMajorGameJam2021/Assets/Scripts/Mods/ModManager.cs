using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    public static ModManager main;
    public static List<Mod> mods;
    void Awake()
    {
        main = this;
        mods = new List<Mod>();
        mods.Add(new ModCold("Cold Mod1", "Decreases Temperature"));
        mods.Add(new ModCold("Cold Mod2", "Decreases Temperature"));
    }
}
