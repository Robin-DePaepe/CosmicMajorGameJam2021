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
    [Tooltip("game time minutes")]
    public float satisfactionRate = 10;
    
    public int corruptPenalty = 10;
    public int destroyPenalty = 20;
    public float speed = 0.1f;
    #endregion

    #region Lists and Dicts

    public Dictionary<GameObject, Planet> planets;
    public List<GameObject> corrupt;
    public List<GameObject> unCorrupt;
    public List<GameObject> blackHoles;
    public List<PlanetData> planetData;

    #endregion

    #region Unity Functions

    void Awake()
    {
        main = this;
        planets = new Dictionary<GameObject, Planet>();
        corrupt = new List<GameObject>();
        unCorrupt = new List<GameObject>();
        planetData = new List<PlanetData>();
        CSVReader.LoadFromString(Resources.Load<TextAsset>("Planets").ToString(), Reader);
    }

    private void Start()
    {
        for (int i = 0; i < planetData.Count; i++)
        {
            StartCoroutine(schedulePlanet(planetData[i]));
        }

        StartCoroutine(addUpPoints());
    }
    
    #endregion

    #region Create And Destroy

    void Reader(int lineIndex, List<string> line)
    {
        if (lineIndex > 0)
        {
            planetData.Add(new PlanetData(line));
        }
    }
    void createPlanet(string planetName, string description, Stats stats)
    {
        GameObject planet = Instantiate(planetTemplate, planetParent.transform);
        Planet planetScript = planet.GetComponent<Planet>();

        planetScript.behaviour.currentAngle = Random.Range(0, 360);
        planetScript.behaviour.maxDistance = maxDistance;
        planetScript.behaviour.minDistance = minDistance;
        planetScript.description = description;
        planetScript.planetName = planetName;
        planetScript.behaviour.travelingSpeed = speed;
        planetScript.baseStats = stats;
        planet.name = planetName;

        planets.Add(planet, planetScript);
        unCorrupt.Add(planet);
        
        planetScript.behaviour.setPosition();
        for (int i = 0; i < 50; i++)
        {
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.NoFilter();
            planetScript.behaviour.col.OverlapCollider(filter, colliders);

            if (colliders.Count > 0)
            {
                planetScript.behaviour.setPosition();
            }
            else
            {
                break;
            }
        }
    }
    public void createBlackHole()
    {
        Vector3 position = new Vector3(Random.Range(minDistance, maxDistance),
            Random.Range(minDistance, maxDistance), 0);

        GameObject blackHole = Instantiate(blackHoleTemplate, planetParent.transform);
        blackHole.transform.position = position;

        blackHoles.Add(blackHole);

        GameManager.main.checkTutorial(tutNames.blackHole);
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
        SatisfactionManager.main.AddSatisfaction(-destroyPenalty);
        Destroy(planet);
    }

    #endregion

    #region Coroutines

    IEnumerator addUpPoints()
    {
        while (gameObject.activeSelf)
        {
            GameTime nextTime = TimeManager.main.currentTime + new GameTime(satisfactionRate*60, 0, 0);
            while (nextTime >= TimeManager.main.currentTime)
            {
                yield return new WaitForEndOfFrame();
            }

            for (int i = 0; i < unCorrupt.Count; i++)
            {
                Planet planet = planets[unCorrupt[i]];
                planet.addPoints();
            }
        }
    }

    IEnumerator schedulePlanet(PlanetData data)
    {
        GameTime scheduledTime = TimeManager.main.currentTime + new GameTime(data.hoursToSpawn * 3600, 0, 0);
        while (scheduledTime >= TimeManager.main.currentTime)
        {
            yield return new WaitForEndOfFrame();
        }

        createPlanet(data.name, data.description, data.stats);
        WindowManager.main.CreatePopUp("New planet: " + data.name, 0, 3f);
    }
    
    #endregion
    
    #region Corruption

    public void CorruptRandom()
    {
        if (unCorrupt.Count > 0)
        {
            GameObject target = unCorrupt[Random.Range(0, unCorrupt.Count)];

            planets[target].behaviour.Corrupt();
        }
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
