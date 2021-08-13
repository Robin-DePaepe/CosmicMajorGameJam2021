
[System.Serializable]
public class StatAirQuality : Stat
{
    public StatAirQuality(int greenPosition, int greenExtent, int PointProduction) : base(greenPosition, greenExtent, PointProduction)
    {
        statName = "Air Quality";
        description = "Quality of Air on the planet";
    }
}
