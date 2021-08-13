using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetManager : MonoBehaviour
{
    #region Components
    
    public static PlanetManager main;
    public GameObject planetTemplate;
    public GameObject planetParent;
    public GameObject blackHoleTemplate;
    
    #endregion

    #region Variables

    public float maxDistance = 50;
    public float minDistance = 5;
    
    public int corruptPenalty = 10;
    public int destroyPenalty = 20;

    #endregion

    #region Lists and Dicts

    public Dictionary<GameObject, Planet> planets;
    public List<GameObject> corrupt;
    public List<GameObject> unCorrupt;
    public List<GameObject> blackHoles;
    
    #endregion

    #region Unity Functions

    void Awake()
    {
        main = this;
        planets = new Dictionary<GameObject, Planet>();
        corrupt = new List<GameObject>();
        unCorrupt = new List<GameObject>();
    }

    private void Start()
    {
        createPlanet("Aferus", 30, 90);
        createPlanet("Aquonda", 20, 0);
        //Invoke(nameof(CorruptRandom), 3f);
        //Invoke(nameof(createBlackHole), 10f);
    }

    void Update()
    {
        
    }

    #endregion

    #region Create And Destroy

    void createPlanet(string planetName, float startDistance, float startAngle)
    {
        GameObject planet = Instantiate(planetTemplate, planetParent.transform);
        Planet planetScript = planet.GetComponent<Planet>();

        planetScript.behaviour.startDistanceToSun = startDistance;
        planetScript.behaviour.currentAngle = startAngle;
        planetScript.behaviour.maxDistance = maxDistance;
        planetScript.behaviour.minDistance = minDistance;
        
        
        planetScript.planetName = planetName;
        planet.name = planetName;
        
        planets.Add(planet, planetScript);
        unCorrupt.Add(planet);
    }

    void createBlackHole()
    {
        Vector3 position = new Vector3(Random.Range(minDistance * 2, maxDistance / 2),
            Random.Range(minDistance * 2, maxDistance / 2), 0);
        
        GameObject blackHole = Instantiate(blackHoleTemplate, planetParent.transform);
        blackHole.transform.position = position;
        
        blackHoles.Add(blackHole);
    }

    public void destroyAllBlackHoles()
    {
        for (int i = 0; i < blackHoles.Count; i++)
        {
            Destroy(blackHoles[i]);
        }
        blackHoles.Clear();
    }
    public void destroyPlanet(GameObject planet)
    {
        planets.Remove(planet);

        if (corrupt.Contains(planet))
        {
            corrupt.Remove(planet);
        }
        else
        {
            unCorrupt.Remove(planet);
        }
        SatisfactionManager.main.ReduceSatisfaction(destroyPenalty);
        Destroy(planet);
    }

    #endregion

    #region Corruption

    public void CorruptRandom()
    {
        GameObject target = unCorrupt[Random.Range(0, unCorrupt.Count)];

        planets[target].behaviour.Corrupt();
    }

    void UnCorruptAll()
    {
        for (int i = 0; i < corrupt.Count; i++)
        {
            if (!planets[corrupt[i]].behaviour.fromCollision)
            {
                planets[corrupt[i]].behaviour.UnCorrupt();
            }
        }
    }

    #endregion

    #region Malware

    public bool checkMalware()
    {
        for (int i = 0; i < corrupt.Count; i++)
        {
            if (!planets[corrupt[i]].behaviour.fromCollision)
            {
                return true;
            }
        }

        if (blackHoles.Count > 0)
        {
            return true;
        }
        
        //check for popups here
        
        return false;
    }

    public void clearMalware()
    {
        UnCorruptAll();
        destroyAllBlackHoles();
        //remove popups here
    }
    #endregion
}
