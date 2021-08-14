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
    internal ShortcutPlanet shortcut;
    public List<Mod> mods;
    public Color halfGood;
    public Color bad;
    private void Awake()
    {
        behaviour = GetComponent<PlanetBehaviour>();
        shortcut = GetComponent<ShortcutPlanet>();
    }

    void Start()
    {
        mods = new List<Mod>();
        
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
        stats.calculatePoints();
        if (stats.pointsProduced == stats.maxProduction)
        {
            shortcut.sprite.color = Color.white;
        }
        else if (stats.pointsProduced >= 0)
        {
            shortcut.sprite.color = halfGood;
        }
        else
        {
            shortcut.sprite.color = bad;
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
                    SatisfactionManager.main.AddSatisfaction(stats.pointsProduced);
                    timeSinceLast = 0;
                }
            }

        }
    }
}
