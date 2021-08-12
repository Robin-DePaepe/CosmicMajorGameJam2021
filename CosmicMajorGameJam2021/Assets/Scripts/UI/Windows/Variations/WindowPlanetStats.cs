using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowPlanetStats : WindowDraggable
{
    public GameObject statTemplate;
    public GameObject statParent;
    public TextMeshProUGUI planetNameText;
    internal Planet planet;
    protected override void Start()
    {
        base.Start();
        planetNameText.text = planet.planetName;
        CreateStats();
    }

    public override void Minimise()
    {
        base.Minimise();
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }

    void CreateStats()
    {
        for (int i = 0; i < planet.stats.list.Count; i++)
        {
            GameObject statObj = Instantiate(statTemplate, statParent.transform);
            StatBar statScript = statObj.GetComponent<StatBar>();
            statScript.planet = planet;
            statScript.index = i;
        }
    }
}
