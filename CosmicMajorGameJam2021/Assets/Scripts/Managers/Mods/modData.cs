using System.Collections.Generic;
using UnityEngine;

public class modData
{
    public string modName;
    public int[] changes = new int[5];
    public int susPoints;
    public string description;
    public bool start;
    public string website;

    public modData(List<string> line)
    {
        bool tryParse;
        modName = line[0];
        for (int i = 0; i < 5; i++)
        {
            int j = i + 1;
            string cell = line[j];

            changes[i] = 0;
            if (cell != "")
            {
                tryParse = int.TryParse(cell.Replace("-",""), out int Parse);
                
                if (!tryParse)
                {
                    Debug.Log("error: " + cell);
                }
                
                if (cell.Contains("-"))
                {
                    changes[i] = -Parse;
                }
                else
                {
                    changes[i] = Parse;
                }
            }
        }

        tryParse = int.TryParse(line[6], out int parse);
        if (!tryParse)
        {
            Debug.Log("error: " + line[6]);
        }

        susPoints = parse;

        description = line[7];

        if (line[8].Contains("x2"))
        {
            start = true;
        }
        else
        {
            website = line[8];
        }
    }
}
