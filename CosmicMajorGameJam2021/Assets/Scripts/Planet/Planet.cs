using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Stats baseStats;
    public Stats stats;
    public string planetName = "Planet";
    internal PlanetBehaviour behaviour;
    public List<Mod> mods;
    void Start()
    {
        mods = new List<Mod>();
        
        behaviour = GetComponent<PlanetBehaviour>();
        baseStats = new Stats(
            new Stat(50,70,1),
            new Stat(50,70,1),
            new Stat(50,70,1),
            new Stat(50,70,1),
            new Stat(50,70,1)
            );
        
        stats = new Stats(baseStats);

        StartCoroutine(waitPoints());
    }

    private void Update()
    {
        stats = new Stats(baseStats);
        for (int i = 0; i < mods.Count; i++)
        {
            mods[i].ChangeStats(stats);
        }
    }

    IEnumerator waitPoints()
    {
        float timeSinceLast = 0;
        while (gameObject.activeSelf)
        {
            yield return new WaitForEndOfFrame();

            if (behaviour.gameObject.activeSelf)
            {
                timeSinceLast += Time.deltaTime;
                
                if (timeSinceLast >= 10f)
                {
                    int pointsProduced = 0;
                    for (int i = 0; i < stats.list.Count; i++)
                    {
                        pointsProduced += stats.list[i].PointsProduced();
                    }
                    SatisfactionManager.main.AddSatisfaction(pointsProduced);
                    timeSinceLast = 0;
                    SatisfactionManager.main.AddSatisfaction(pointsProduced);
                }
            }

        }
    }
}
