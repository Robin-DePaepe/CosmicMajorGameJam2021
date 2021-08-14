using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlanetBehaviour : MonoBehaviour
{
    #region Components and Constants
    public const float travelingSpeed = 3f;
    const float screenRatio = 16f / 9f;

    [Header("Components")]
    public Sprite unCorruptSprite;
    public Sprite corruptSprite;
    static Camera gameCamera;
    CircleCollider2D col;
    internal SpriteRenderer sprite;
    #endregion

    #region Variables
    
    [Header("Variables")]
    public float startDistanceToSun;
    public float currentAngle;
    public float offsetDistanceToSun = 0;
    public float maxDistance = 50;
    public float minDistance = 5;

    private float lastDistance;
    internal bool frozen;
    bool corrupted;
    internal bool fromCollision;
    private bool startCol;
    
    #endregion

    #region Unity Events

    private void Awake()
    {
        if (!gameCamera)
        {
            gameCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        StartCoroutine(waitTillStartCol());
    }


    private void Update()
    {
        
        if (!frozen)
        {
            if (corrupted)
            {
                Vector3 oldPos = transform.position;
                transform.position = getNextPos();

                if (!CheckFarther())
                {
                    transform.position = oldPos;
                }
            }
            else
            {
                nextAngle();
                transform.position = getNextPos();
            }
        }

    }


    #endregion

    #region Movement

    void nextAngle()
    {
        currentAngle += travelingSpeed * TimeManager.main.ConvertRealTimeToGameTime(Time.deltaTime);
        
        if (currentAngle >= 360)
        {
            currentAngle -= 360;
        }
    }

    Vector3 getNextPos()
    {
        Vector3 centralPos = new Vector3(0, 0, 0);
        Vector3 dir = new Vector3(1, 0, 0);

        dir = Quaternion.Euler(0f, 0f, currentAngle) * dir;

        centralPos += (startDistanceToSun + offsetDistanceToSun) * dir;
        centralPos.x *= screenRatio;

        return centralPos;
    }

    bool CheckFarther()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        for (int i = 0; i < col.OverlapCollider(filter, colliders); i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                float currentDistance = Vector3.Distance(colliders[i].transform.position, transform.position);

                if (currentDistance > lastDistance)
                {
                    return true;
                }
            }
        }

        return false;
    }
    IEnumerator waitTillStartCol()
    {
        yield return new WaitForSeconds(0.3f);
        startCol = true;
    }
    #endregion

    #region Get/Set

    public void SetDistance(float offset)
    {
        offsetDistanceToSun = offset;
        transform.position = getNextPos();
    }

    public float getMin()
    {
        return minDistance - startDistanceToSun;
    }

    public float getMax()
    {
        return maxDistance - startDistanceToSun;
    }

    #endregion

    #region Triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Planet") && startCol)
        {
            PlanetBehaviour otherPlanet = PlanetManager.main.planets[other.gameObject].behaviour;

            if (!otherPlanet.corrupted)
            {
                otherPlanet.Corrupt(true);
            }
            lastDistance = Vector3.Distance(other.transform.position, transform.position);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Planet"))
        {
            PlanetBehaviour otherPlanet = PlanetManager.main.planets[other.gameObject].behaviour;

            if (otherPlanet.fromCollision)
            {
                otherPlanet.UnCorrupt(true);
            }
        }
    }

    #endregion

    #region Corrupt

    public void Corrupt(bool collision = false)
    {
        corrupted = true;
        SatisfactionManager.main.ReduceSatisfaction(PlanetManager.main.corruptPenalty);
        sprite.sprite = corruptSprite;
        
        PlanetManager.main.unCorrupt.Remove(gameObject);
        PlanetManager.main.corrupt.Add(gameObject);
        
        if (collision)
        {
            fromCollision = true;
        }
        SoundManager.main.PlaySoundEffect(SoundEffects.foldercorruption);
    }

    public void UnCorrupt(bool collision = false)
    {
        corrupted = false;

        sprite.sprite = unCorruptSprite;
        
        PlanetManager.main.corrupt.Remove(gameObject);
        PlanetManager.main.unCorrupt.Add(gameObject);
        
        if (collision)
        {
            fromCollision = false;
            startCol = false;

            StartCoroutine(waitTillStartCol());
        }
    }

    #endregion

}
