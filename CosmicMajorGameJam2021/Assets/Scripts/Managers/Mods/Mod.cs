using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Mod
{
    internal string modName;
    internal string description;
    internal Sprite icon;
    internal List<int> changes;
    internal string website;
    private int susPoints;
    public Mod(modData data)
    {
        modName = data.modName;
        susPoints = data.susPoints;

        description = data.description;
        icon = Resources.Load<Sprite>("ModIcon");
        changes = data.changes.ToList();

        if (!data.start)
        {
            website = data.website;
        }
        else
        {
            website = "";
        }
    }

    public void AddSuspicion()
    {
        SuspicionManager.main.AddSuspicion(susPoints);
    }

    public virtual void ChangeStats(Stats stat)
    {
        for (int i = 0; i < stat.list.Count; i++)
        {
            stat.list[i].barProgress += changes[i];
            stat.list[i].barProgress = Mathf.Clamp(stat.list[i].barProgress, 0, 100);
        }
    }
}
