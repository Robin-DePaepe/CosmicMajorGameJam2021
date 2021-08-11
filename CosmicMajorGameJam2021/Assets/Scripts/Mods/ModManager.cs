using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    public static ModManager main;
    public List<Mod> modInspector;
    public static List<Mod> mods;
    void Awake()
    {
        main = this;
        mods = modInspector;
    }
}
