[System.Serializable]
public class Stat
{
    public int barProgress;
    public int greenMin;
    public int greenMax;
    public string statName = "Statistic";
    public string description = "Description";
    int PointProduction;
    int buffer;

    public Stat(int greenMin, int greenMax, int PointProduction)
    {
        barProgress = greenMin + (greenMax-greenMin)/2;
        this.greenMin = greenMin;
        this.greenMax = greenMax;
        this.PointProduction = PointProduction;
        buffer = CalculateBuffer();
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
            return PointProduction;
        }
        if (barProgress >= greenMin - buffer && barProgress <= greenMax + buffer)
        {
            return 0;
        }

        return -1;
    }

    public int CalculateBuffer()
    {
        return (greenMax-greenMin)/2;
    }
}
