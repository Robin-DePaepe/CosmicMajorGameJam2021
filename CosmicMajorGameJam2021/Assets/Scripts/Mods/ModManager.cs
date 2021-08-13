using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    public static ModManager main;
    public static List<Mod> mods;
    public static Dictionary<string, Mod> allMods;
    public List<modData> modData;
    private TextAsset sheet;
    void Start()
    {
        main = this;

        modData = new List<modData>();
        sheet = Resources.Load<TextAsset>("Mods");
        CSVReader.LoadFromString(sheet.text, Reader);

        allMods = new Dictionary<string, Mod>();
        mods = new List<Mod>();
        for (int i = 0; i < modData.Count; i++)
        {
            Mod mod = new Mod(modData[i]);
            allMods.Add(mod.modName, mod);

            if (modData[i].start)
            {
                mod.AddSuspicion();
                mods.Add(mod);
                mods.Add(mod);
            }
        }
    }

    void Reader(int lineIndex, List<string> line)
    {
        if (lineIndex > 0)
        {
            modData.Add(new modData(line));
        }
    }
}
