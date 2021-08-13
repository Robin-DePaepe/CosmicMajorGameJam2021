using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlanetBehaviour : MonoBehaviour
{
    #region variable setup

    //movement
    [SerializeField] bool canMove = true;

    public float startDistanceToSun;

    [SerializeField] private float travelingSpeed;
    [SerializeField] private float currentAngle;
    public float offsetDistanceToSun = 0;
    private const float screenRatio = 16f / 9f;
    public float maxDistance = 50;
    public float minDistance = 5;
    [SerializeField] CircleCollider2D innerCol;
    //property window
    static Camera gameCamera;
    #endregion

    #region properties

    private float lastDistance;
    internal bool frozen;
    bool CanMove;
    public bool corrupted;
    
    
    #endregion

    private void Start()
    {
        if (!gameCamera)
        {
            gameCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        innerCol = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {

        canMove = !corrupted;
        
        if (!frozen)
        {
            if (!canMove)
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
        for (int i = 0; i < innerCol.OverlapCollider(filter, colliders); i++)
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Planet"))
        {
            other.GetComponent<PlanetBehaviour>().corrupted = true;
            lastDistance = Vector3.Distance(other.transform.position, transform.position);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Planet"))
        {
            other.GetComponent<PlanetBehaviour>().corrupted = false;
        }
    }

}
