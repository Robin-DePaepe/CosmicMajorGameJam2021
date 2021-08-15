using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class ShortcutPlanet : Shortcut
{
    private PlanetBehaviour planetBehaviour;
    internal Planet planet;
    private Vector3 offset;
    private BoxCollider2D col;
    private WindowModPlanets windowScript;
    internal SpriteRenderer sprite;
    public SpritePair unCorruptSprite;
    public SpritePair collisionSprite;
    public List<SpritePair> corruptSprites;
    public float clickTime = 0.2f;
    public TextMeshProUGUI nameText;
    public Canvas worldCanvas;
    SpritePair currentPair;
    internal Sprite thumb;

    private bool clicked;
    
    #region Unity

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        planetBehaviour = GetComponent<PlanetBehaviour>();
        worldCanvas.worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        col = GetComponent<BoxCollider2D>();
        offset = new Vector3(col.bounds.extents.x, 0, 0);
        planet = GetComponent<Planet>();
        manager = WindowManager.main;
    }

    protected override void Start()
    {
        nameText.text = planet.planetName;
        thumb = Resources.Load<Sprite>("PlanetThumbs/" + planet.planetName.ToLower());
        SetPair(unCorruptSprite);
    }
    
    private void Update()
    {
        if (window)
        {
            planetBehaviour.frozen = window.activeSelf;
        }
    }

    #endregion

    #region Window/Shortcut

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.main.firstPlanetOpen)
        {
            GameManager.main.firstPlanetOpen = true;
            WindowManager.main.createTutorial(GameManager.main.firstPlanetOpenText);
        }

        if (window)
        {
            if (window.activeSelf)
            {
                window.SetActive(false);
            }
            else
            {
                spriteOpen();
                window.SetActive(true);
                OffSetPosition();
            }
        }
        else
        {
            spriteOpen();
            CreateWindow();
        }


    }

    protected override void CreateWindow()
    {
        window = manager.CreateWindow(transform.position + offset, windowTemplate,check:false, Icon:thumb);
        windowScript = (WindowModPlanets)manager.windows[window].script;
        windowScript.planet = planet;
        OffSetPosition();
    }
    void OffSetPosition()
    {
        Canvas.ForceUpdateCanvases();
        RectTransform windowRect = window.GetComponent<RectTransform>();
        window.transform.position = transform.position + offset+ new Vector3((windowRect.rect.width / 2) * windowRect.lossyScale.x, -(windowRect.rect.height / 2) * windowRect.lossyScale.y);
        manager.CorrectPosition(windowRect);
    }

    #endregion

    #region Sprites

    public void SetCorrupted(bool collision)
    {
        if (collision)
        {
            SetPair(collisionSprite);
        }
        else
        {
            sprite.sprite = corruptSprites[Random.Range(0, corruptSprites.Count)].open;
        }
    }

    void spriteOpen()
    {
        if (!clicked)
        {
            StartCoroutine(waitUnClick());
        }
    }

    public void SetPair(SpritePair pair, bool open = false)
    {
        currentPair = pair;
        if (open)
        {
            sprite.sprite = pair.open;
        }
        else
        {
            sprite.sprite = pair.close;
        }
    }
    IEnumerator waitUnClick()
    {
        clicked = true;
        SpritePair startPair = currentPair;
        SetPair(startPair, true);
        
        yield return new WaitForSeconds(clickTime);
        clicked = false;
        if (startPair == currentPair)
        {
            SetPair(startPair);
        }
    }
    
    #endregion
}

[System.Serializable]
public class SpritePair
{
    public Sprite close;
    public Sprite open;

    public SpritePair(Sprite close, Sprite open)
    {
        this.close = close;
        this.open = open;
    }
}
