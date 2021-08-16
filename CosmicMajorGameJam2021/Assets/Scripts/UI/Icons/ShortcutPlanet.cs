using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class ShortcutPlanet : Shortcut
{
    #region Components
    
    [Header("Components")]
    private PlanetBehaviour planetBehaviour;
    internal Planet planet;
    private WindowModPlanets windowScript;
    internal SpriteRenderer sprite;
    public Canvas worldCanvas;
    private BoxCollider2D col;

    [Header("Sprites")]
    public SpritePair unCorruptSprite;
    public SpritePair collisionSprite;
    public List<SpritePair> corruptSprites;
    internal Sprite thumb;

    
    [Header("Text")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI addPointText;
    #endregion

    #region Variables
    private Vector3 offset;
    public float clickTime = 0.2f;
    public float pointStay;
    public float pointFadeTime;
    private bool clicked;
    SpritePair currentPair;
    #endregion
    

    
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

        if (planet)
        {
            pointText.text = planet.stats.pointsProduced + "/" + planet.stats.maxProduction;
            pointText.color = sprite.color + (Color.white/2);
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
            SetPair(corruptSprites[Random.Range(0, corruptSprites.Count)]);
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
            sprite.sprite = currentPair.open;
        }
        else
        {
            sprite.sprite = currentPair.close;
        }
    }
    IEnumerator waitUnClick()
    {
        clicked = true;
        SetPair(currentPair,true);
        
        yield return new WaitForSeconds(clickTime);
        clicked = false;
        
        SetPair(currentPair);
    }
    
    #endregion

    #region Other

    public void AddPoints(int points)
    {
        addPointText.color = pointText.color;
        string pointString = points.ToString();
        if (points >= 0)
        {
            pointString = "+" + pointString;
        }
        addPointText.text = pointString;
        addPointText.gameObject.SetActive(true);
        StartCoroutine(waitDisablePoints());
    }

    IEnumerator waitDisablePoints()
    {
        float timeSinceStart = 0;
        while (addPointText.gameObject.activeSelf)
        {
            if (!TimeManager.main.timePaused)
            {
                timeSinceStart += Time.deltaTime;

                if (timeSinceStart >= pointStay)
                {
                    float alpha  = 1-((timeSinceStart - pointStay)/pointFadeTime);
                    Color og = addPointText.color;
                    addPointText.color = new Color(og.r, og.g, og.b, alpha);
                
                    if (timeSinceStart-pointStay >= pointFadeTime)
                    {
                        addPointText.gameObject.SetActive(false);
                    }
                }
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
}

[Serializable]
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
