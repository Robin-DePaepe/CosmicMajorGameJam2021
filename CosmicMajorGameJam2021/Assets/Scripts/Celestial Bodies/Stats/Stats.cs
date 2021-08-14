
using System.Collections.Generic;
[System.Serializable]
public class Stats
{
    public List<Stat> list;
    public int pointsProduced;
    public int maxProduction;
    public Stat temperature;
    public Stat waterLevels;
    public Stat vegetation;
    public Stat airQuality;
    public Stat population;

    public Stats(Stat temperature, Stat waterLevels, Stat vegetation, Stat population, Stat airQuality)
    {
        temperature.SetStat("Temperature", "Ambient temperature of planet");
        this.temperature = temperature;
        
        waterLevels.SetStat("Water Levels", "Height of sea level");
        this.waterLevels = waterLevels;
        
        vegetation.SetStat("Vegetation", "Amount of greenery");
        this.vegetation = vegetation;
        
        population.SetStat("Population", "Population density");
        this.population = population;
        
        airQuality.SetStat("Air Quality", "Quality of Air on the planet");
        this.airQuality = airQuality;
        
        createList();
        setMaxProduction();
    }

    public Stats(Stats original)
    {
        temperature = new Stat(original.temperature);
        temperature.SetStat("Temperature", "Ambient temperature of planet");

        waterLevels = new Stat(original.waterLevels);
        waterLevels.SetStat("Water Levels", "Height of sea level");

        vegetation = new Stat(original.vegetation);
        vegetation.SetStat("Vegetation", "Amount of greenery");

        airQuality = new Stat(original.airQuality);
        airQuality.SetStat("Air Quality", "Quality of Air on the planet");

        population = new Stat(original.population);
        population.SetStat("Population", "Population density");

        createList();
        setMaxProduction();
    }

    public void calculatePoints()
    {
        pointsProduced = 0;

        for (int i = 0; i < list.Count; i++)
        {
            pointsProduced += list[i].PointsProduced();
        }
    }

    void createList()
    {
        list = new List<Stat>();
        list.Add(temperature);
        list.Add(waterLevels);
        list.Add(vegetation);
        list.Add(population);
        list.Add(airQuality);
    }

    void setMaxProduction()
    {
        for (int i = 0; i < list.Count; i++)
        {
            maxProduction += list[i].PointProduction;
        }
    }
}
