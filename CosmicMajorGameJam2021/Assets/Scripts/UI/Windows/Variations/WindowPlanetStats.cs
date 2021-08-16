using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowPlanetStats : WindowDraggable
{
    public GameObject statTemplate;
    public GameObject statParent;
    public WindowPlanetShower planetShower;
    public TextMeshProUGUI planetNameText;
    internal Planet planet;

    protected override void Start()
    {
        base.Start();
        planet.statWindow = gameObject;
        planetShower.planet = planet;
        planetNameText.text = planet.planetName;
        CreateStats();
    }

    public override void Minimise()
    {
        if (planet.modWindow)
        {
            planet.modWindow.SetActive(false);
        }
        gameObject.SetActive(false);
        base.Minimise();
    }

    private void OnEnable()
    {
        if (!planetShower.gameObject.activeSelf && planet != null)
        {
            planetShower.gameObject.SetActive(true);
        }
        if (planet)
        {
            if (planet.modWindow)
            {
                planet.modWindow.SetActive(true);
            }
        }
    }

    public override void Close()
    {
        if (planet.modWindow)
        {
            Destroy(planet.modWindow); 
        }
        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        if (!planet)
        {
            Destroy(gameObject);
        }
        planetNameText.text = planet.planetName + ": " + planet.stats.pointsProduced + "/" + planet.stats.maxProduction;
    }

    void CreateStats()
    {
        for (int i = 0; i < planet.stats.list.Count; i++)
        {
            if (planet.stats.list[i] != null)
            {
                GameObject statObj = Instantiate(statTemplate, statParent.transform);
                StatBar statScript = statObj.GetComponent<StatBar>();
                statScript.planet = planet;
                statScript.index = i;
            }
        }
    }
}
