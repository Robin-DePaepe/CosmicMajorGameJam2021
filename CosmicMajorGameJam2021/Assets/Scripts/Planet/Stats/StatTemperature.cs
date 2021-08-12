[System.Serializable]
public class StatTemperature : Stat
{
    public StatTemperature(int greenPosition, int greenExtent, int PointProduction) : base(greenPosition, greenExtent, PointProduction)
    {
        statName = "Air Quality";
        description = "Quality of Air on the planet";
    }
}
