
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
        stats = new Stats(
            new Stat(getRanges(line[4]), difficulty),
            new Stat(getRanges(line[5]), difficulty),
            new Stat(getRanges(line[6]), difficulty),
            new Stat(getRanges(line[7]), difficulty),
            new Stat(getRanges(line[8]), difficulty)
        );
    }

    int[] getRanges(string cell)
    {
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
