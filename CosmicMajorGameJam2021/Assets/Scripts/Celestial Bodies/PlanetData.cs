
using System.Collections.Generic;
using UnityEngine;

public class PlanetData
{
    public string name;
    public string description;
    public Stats stats;
    public float hoursToSpawn;
    
    public PlanetData(List<string> line)
    {
        hoursToSpawn = tryFloatParse(line[0]);
        name = line[1];
        description = line[2];
        string difficulty = line[3];

        List<Stat> statsToAdd = new List<Stat>();
        for (int i = 4; i < 9; i++)
        {
            int[] ranges = getRanges(line[i]);
            if (ranges != null)
            {
                statsToAdd.Add(new Stat(ranges, difficulty));
            }
            else
            {
                statsToAdd.Add(null);
            }
        }

        stats = new Stats(statsToAdd);
    }

    int[] getRanges(string cell)
    {
        if (cell == "")
        {
            return null;
        }
        int[] ranges = new int[2];
        string[] stringRanges = cell.Split('-');

        for (int i = 0; i < ranges.Length; i++)
        {
            ranges[i] = tryParse(stringRanges[i]);
        }
        
        return ranges;
    }

    int tryParse(string toParse)
    {
        if (!int.TryParse(toParse, out int outInt))
        {
            Debug.Log("error parsing: " + toParse);
        } 

        return outInt;
    }

    float tryFloatParse(string toParse)
    {
        if (!float.TryParse(toParse, out float outFloat))
        {
            Debug.Log("error parsing: " + toParse);
        }

        return outFloat;
    }
}
