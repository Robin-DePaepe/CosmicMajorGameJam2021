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
        mods = planet.mods;
        planetNameText.text = planet.planetName;
        GameObject shortcutObj = Instantiate(statShortcutTemplate, modParent.transform);
        shortcut = shortcutObj.GetComponent<ShortcutPlanetStat>();
        shortcut.manager = manager;
        shortcut.transform.SetAsFirstSibling();
        shortcut.planet = planet;

    }

    public override void Minimise()
    {
        base.Minimise();
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
    public override void AddMod(GameObject modObject, Mod mod)
    {
        base.AddMod(modObject, mod);
        shortcut.transform.SetAsFirstSibling();
    }
    protected override void CreateMod(Mod mod)
    {
        base.CreateMod(mod);
    }


}
