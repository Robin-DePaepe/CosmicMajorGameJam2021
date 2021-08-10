using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlanetBehaviour : MonoBehaviour, IPointerClickHandler
{
    #region variable setup

    //movement
    [SerializeField] bool canMove = true;

    [SerializeField] private GameObject distanceSlider;
    [SerializeField] private float startDistanceToSun;
    [SerializeField] private float changableDistanceOffset;
    private float offsetDistanceToSun = 0;

    [SerializeField] private GameObject speedSlider;
    [SerializeField] private float travelingSpeed;
    [SerializeField] private float currentAngle;
    private float speedBoost;

    private const float screenRatio = 16f / 9f;

    //property window
    [SerializeField] private GameObject planetPropertyWindow;
    bool playerInPropertyWindow = false;

    static Camera gameCamera;
    #endregion

    #region properties
    public float OffsetDistanceToSun
    {
        get { return offsetDistanceToSun; }
        set { offsetDistanceToSun = value; }
    }
    public float TravelingSpeed
    {
        get { return travelingSpeed; }
        set { travelingSpeed = value; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
    #endregion

    private void Start()
    {
        if (!gameCamera)
        {
            gameCamera = FindObjectOfType<Camera>();
        }
        if (distanceSlider)
        {
            distanceSlider.GetComponent<Slider>().minValue = -changableDistanceOffset;
            distanceSlider.GetComponent<Slider>().maxValue = changableDistanceOffset;
            distanceSlider.GetComponent<Slider>().value = 0;
        }
        if (speedSlider)
        {
            speedSlider.GetComponent<Slider>().minValue = -Mathf.Abs(travelingSpeed) / 2f;
            speedSlider.GetComponent<Slider>().maxValue = Mathf.Abs(travelingSpeed);
            speedSlider.GetComponent<Slider>().value = 0;
        }
        if (planetPropertyWindow)
        {
            planetPropertyWindow.transform.SetParent(FindObjectOfType<Canvas>().transform);
            planetPropertyWindow.SetActive(false);
        }

        
    }
    private void Update()
    {
        //if we can move then we will calculate the new position of the planet and teleport to it
        if (canMove)
        {
            currentAngle += (travelingSpeed + speedBoost) * Time.deltaTime;

            Vector3 centralPos = new Vector3(0, 0, 0);
            Vector3 dir = new Vector3(1, 0, 0);

            dir = Quaternion.Euler(0f, 0f, currentAngle) * dir;

            centralPos += (startDistanceToSun + offsetDistanceToSun) * dir;
            centralPos.x *= screenRatio;

            transform.position = centralPos;
        }

        if (playerInPropertyWindow)
        {
            planetPropertyWindow.transform.position = gameCamera.WorldToScreenPoint(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            other.GetComponent<PlanetBehaviour>().CanMove = false;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        planetPropertyWindow.SetActive(true);
        playerInPropertyWindow = true;
    }

    public void ClosePropertyWindow()
    {
        planetPropertyWindow.SetActive(false);
        playerInPropertyWindow = false;
    }

    public void OnSpeedChanged(float speed)
    {
        speedBoost = speed * Mathf.Sign(travelingSpeed);
    }

    public void OnDistanceChanged(float distance)
    {
        offsetDistanceToSun = distance;
    }
}
