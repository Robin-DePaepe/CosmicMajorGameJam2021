using UnityEngine;

[System.Serializable]
public class Stat
{
    public int barProgress;
    public int greenMin;
    public int greenMax;
    public string statName = "Statistic";
    public string description = "Description";
    internal int PointProduction;
    int buffer;
    internal int pointsProduced;
    public Stat(int greenMin, int greenMax, int PointProduction)
    {
        this.greenMin = greenMin;
        this.greenMax = greenMax;
        this.PointProduction = PointProduction;
        buffer = CalculateBuffer();
        barProgress = Random.Range(0,101);
    }

    public Stat(int[] ranges, string difficulty)
    {
        greenMin = ranges[0];
        greenMax = ranges[1];

        if (difficulty == "Hard")
        {
            PointProduction = 3;
        }
        else if (difficulty == "Medium")
        {
            PointProduction = 2;
        }
        else
        {
            PointProduction = 1;
        }
        barProgress = Random.Range(0,101);
    }

    public Stat(Stat stat)
    {
        barProgress = stat.barProgress;
        greenMin = stat.greenMin;
        greenMax = stat.greenMax;
        PointProduction = stat.PointProduction;
        buffer = CalculateBuffer();
    }
    public void SetStat(string nameParam, string descriptionParam)
    {
        statName = nameParam;
        description = descriptionParam;
    }
    public int PointsProduced()
    {
        buffer = CalculateBuffer();
        if (barProgress >= greenMin && barProgress <= greenMax)
        {
            pointsProduced = PointProduction;
        }
        else if (barProgress >= greenMin - buffer && barProgress <= greenMax + buffer)
        {
            pointsProduced = 0;
        }
        else
        {
            pointsProduced = -PointProduction;
        }

        return pointsProduced;
    }

    public int CalculateBuffer()
    {
        return Mathf.Clamp((greenMax-greenMin)/2,0,100);
    }
}
