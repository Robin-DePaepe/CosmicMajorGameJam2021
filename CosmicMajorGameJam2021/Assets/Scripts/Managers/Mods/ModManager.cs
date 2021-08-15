using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    public static ModManager main;
    public List<Mod> mods;
    public Dictionary<string, Mod> allMods;
    public WindowModStorage modWindow;
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
                //mods.Add(mod);
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

    public void AddModBySite(string siteAddress)
    {
        List<string> availableMods = new List<string>();
        for (int i = 0; i < allMods.Count; i++)
        {
            if (allMods.Values.ToList()[i].website.ToLower() == siteAddress)
            {
                availableMods.Add(allMods.Keys.ToList()[i]);
            }
        }

        if (availableMods.Count > 0)
        {
            TimeManager.main.ScheduleModDownLoad(availableMods[Random.Range(0,availableMods.Count)]);
        }
    }
    public void AddModByName(string modString)
    {
        Mod mod = allMods[modString];
        
        mods.Add(mod);
        mod.AddSuspicion();

        if (modWindow)
        {
            modWindow.CreateMod(mod);
        }
    }
}
