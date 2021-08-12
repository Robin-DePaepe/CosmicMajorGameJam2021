
using UnityEngine;

public class ModCold : Mod
{
    public ModCold(string modName, string description) : base(modName, description)
    {
        icon = Resources.Load<Sprite>("Icon");
    }

    public override void ChangeStats(Stats stat)
    {
        base.ChangeStats(stat);
        stat.temperature.barProgress -= 20;
    }
}
