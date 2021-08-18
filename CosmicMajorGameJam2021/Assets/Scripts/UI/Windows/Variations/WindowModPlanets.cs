using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowModPlanets : WindowMods
{
    public GameObject statShortcutTemplate;
    private ShortcutPlanetStat shortcut;
    public TextMeshProUGUI planetNameText;
    internal Planet planet;
    protected override void Update()
    {
        base.Update();
        if (!planet)
        {
            Destroy(gameObject);
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        planet.modWindow = gameObject;
        mods = planet.mods;
        planetNameText.text = planet.planetName;
        statShortcut();

        for (int i = 0; i < mods.Count; i++)
        {
            CreateMod(mods[i]);
        }
    }

    void statShortcut()
    {
        GameObject shortcutObj = Instantiate(statShortcutTemplate, modParent.transform);
        shortcut = shortcutObj.GetComponent<ShortcutPlanetStat>();
        shortcut.manager = manager;
        shortcut.transform.SetAsFirstSibling();
        shortcut.planet = planet;

    }

    private void OnEnable()
    {
        if (planet)
        {
            if (planet.statWindow)
            {
                planet.statWindow.SetActive(true);
            }
        }
    }

    public override void Minimise()
    {
        if (planet.statWindow)
        {
            planet.statWindow.SetActive(false);
        }
        gameObject.SetActive(false);
        base.Minimise();
    }

    public override void Close()
    {
        if (planet.statWindow)
        {
            Destroy(planet.statWindow);
        }
        Destroy(gameObject);
    }
    public override void AddMod(GameObject modObject, Mod mod)
    {
        base.AddMod(modObject, mod);
        shortcut.transform.SetAsFirstSibling();
    }
    public override void CreateMod(Mod mod)
    {
        base.CreateMod(mod);
    }


}
