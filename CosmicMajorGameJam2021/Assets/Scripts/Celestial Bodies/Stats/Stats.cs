
using System.Collections.Generic;
using UnityEngine;

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

    public Stats(List<Stat> stat)
    {
        if (stat[0] != null)
        {
            stat[0].SetStat("Temperature", "Ambient temperature of planet");
            this.temperature = stat[0];
        }

        if (stat[1] != null)
        {
            stat[1].SetStat("Water Levels", "Height of sea level");
            this.waterLevels = stat[1];
        }

        if (stat[2] != null)
        {
            stat[2].SetStat("Vegetation", "Amount of greenery");
            this.vegetation = stat[2];
        }

        if (stat[3] != null)
        {
            stat[3].SetStat("Population", "Population density");
            this.population = stat[3];
        }

        if (stat[4] != null)
        {
            stat[4].SetStat("Air Quality", "Quality of Air on the planet");
            this.airQuality = stat[4];
        }

        createList();
        setMaxProduction();
    }

    public Stats(Stats original)
    {
        if (original.temperature != null)
        {
            temperature = new Stat(original.temperature);
            temperature.SetStat("Temperature", "Ambient temperature of planet");
        }

        if (original.waterLevels != null)
        {
            waterLevels = new Stat(original.waterLevels);
            waterLevels.SetStat("Water Levels", "Height of sea level");
        }

        if (original.vegetation != null)
        {
            vegetation = new Stat(original.vegetation);
            vegetation.SetStat("Vegetation", "Amount of greenery");
        }

        if (original.airQuality != null)
        {
            airQuality = new Stat(original.airQuality);
            airQuality.SetStat("Air Quality", "Quality of Air on the planet");
        }

        if (original.population != null)
        {
            population = new Stat(original.population);
            population.SetStat("Population", "Population density");
        }

        createList();
        setMaxProduction();
    }

    public void calculatePoints()
    {
        pointsProduced = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                pointsProduced += list[i].PointsProduced();
            }
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
            if (list[i] != null)
            {
                maxProduction += list[i].PointProduction;
            }
        }
    }
}
