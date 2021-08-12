[System.Serializable]
public class Stat
{
    public int barProgress;
    public int greenPosition;
    public int greenExtent;
    public string statName = "Statistic";
    public string description = "Description";
    int PointProduction;
    int buffer;

    public Stat(int greenPosition, int greenExtent, int PointProduction)
    {
        barProgress = greenPosition + (greenExtent/2);
        this.greenPosition = greenPosition;
        this.greenExtent = greenExtent;
        this.PointProduction = PointProduction;
        buffer = CalculateBuffer();
    }

    public Stat(Stat stat)
    {
        barProgress = stat.barProgress;
        greenPosition = stat.greenPosition;
        greenExtent = stat.greenExtent;
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
        if (barProgress >= greenPosition && barProgress <= greenPosition + greenExtent)
        {
            return PointProduction;
        }
        if (barProgress >= greenPosition - buffer && barProgress <= greenPosition + greenExtent + buffer)
        {
            return 0;
        }

        return -1;
    }

    public int CalculateBuffer()
    {
        return ((greenPosition + greenExtent) - greenPosition)/2;
    }
}
