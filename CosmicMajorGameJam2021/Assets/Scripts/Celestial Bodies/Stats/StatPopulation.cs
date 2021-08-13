
[System.Serializable]
public class StatPopulation : Stat
{
    public StatPopulation(int greenPosition, int greenExtent, int PointProduction) : base(greenPosition, greenExtent, PointProduction)
    {
        statName = "Population";
        description = "Population density";
    }
}
