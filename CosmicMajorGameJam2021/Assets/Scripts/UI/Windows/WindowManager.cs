using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    //Ensure there is a camera attached to the Canvas Component
    //Ensure that the camera has the tag "MainCamera"

    #region Components
    
    private Canvas canvasComponent;
    
    [Header("Window Types")]
    public GameObject normal;
    public GameObject hazardous;
    public GameObject conjoined;
    [Header("Window Parents")]
    public GameObject windowParent;
    public GameObject onTopParent;
    
    [Header("Other Components")]
    public GameObject minimisedWindows;
    public GameObject minimisedTemplate;
    public GameObject canvas;
    public RectTransform taskBar; //for getting the position of the taskbar to set the edges of the screen
    Camera main;

    #endregion

    #region Variables
    [Header("Variables")]
    public float offset; //offset between the window's border and edge of screen

    #endregion

    #region Unity Functions

    void Start()
    {
        main = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        canvasComponent = canvas.GetComponent<Canvas>();
        
        //just creating test windows, can change if needed
        CreateWindow(Vector3.zero, "Example 1", normal); 
        CreateWindow(new Vector3(1000,1000,0), "Hazardous", hazardous, true);
    }

    #endregion

    #region Window Functions

    public void CreateWindow(Vector3 position, string windowName, GameObject windowTemplate, bool onTop = false) 
    {
        //creates a window with the location and name specified
        //the location is then checked for remaining within the screen
        //the name is only for displaying at the taskbar, this can be changed to an icon
        
        GameObject window = Instantiate(windowTemplate, position, Quaternion.identity, windowParent.transform);

        if (onTop)
        {
            window.transform.SetParent(onTopParent.transform);
        }
        
        RectTransform windowRect = window.GetComponent<RectTransform>();
        
        windowRect.localPosition = position;

        for (int i = 0; i < 2; i++)
        {
            //checks if withinScreen two times
            //as there is the chance that the window is outside in both x and y and the function can only return one at a time
            //if withinScreen doesn't return null then sets the position of the window to be the correct position
            
            Vector3? within = withinScreen(windowRect);
            if (within != null)
            {
                Vector3 correctedPosition = (Vector3) within;
                correctedPosition.z = 0;
                windowRect.position = correctedPosition;
            }
        }

        window.transform.SetAsLastSibling();
        
        Window script = window.GetComponent<Window>();
        script.manager = this;
        script.windowName = windowName;
    }
    
    public void MinimiseWindow(Window windowScript)
    {
        //minimises the window by disabling the window object and creating a new taskbar icon
        GameObject window = windowScript.gameObject;
        window.SetActive(false);
        GameObject minimised = Instantiate(minimisedTemplate, Vector3.zero, Quaternion.identity, minimisedWindows.transform);
        
        MinimisedWindow buttonScript = minimised.GetComponent<MinimisedWindow>();
        buttonScript.window = windowScript;
    }

    #endregion

    #region Other Functions

    public Vector3? withinScreen(RectTransform rect)
    {
        //checks if all sides of the window are within the screen bounds
        //returns null if within the screen
        //if not within, returns the world position corrected to the border of the screen + offset to prevent the window from being locked
        
        float widthMax = Screen.width;
        float widthMin = 0;
        float heightMax = Screen.height;
        float heightMin = taskBar.rect.height * canvasComponent.scaleFactor;
        //get edges of screen

        Vector2 size = rect.sizeDelta * (canvasComponent.scaleFactor);
        Vector2 extents = size * 0.5f;
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(main, rect.position);
        //get size and extent of screen in Screen coordinates (pixels)

        Vector2 topRight = extents + pos;
        Vector2 bottomLeft = pos - extents;
        //sets bottom left and top right for comparing with the screen edges

        if (topRight.y >= heightMax)
        {
            Vector3 correctedPosition = new Vector3(pos.x, heightMax - extents.y); //extents are added or subtracted to get the centred position
            Vector3 offsetVector = new Vector3(0, -offset, 0); //offset added to prevent window from being locked
            
            return main.ScreenToWorldPoint(correctedPosition + offsetVector); //returns world point
        }

        if (topRight.x >= widthMax)
        {
            Vector3 correctedPosition = new Vector3(widthMax - extents.x, pos.y);
            Vector3 offsetVector = new Vector3(-offset, 0, 0);
            
            return main.ScreenToWorldPoint(correctedPosition + offsetVector);        
        }

        if (bottomLeft.x <= widthMin)
        {
            Vector3 correctedPosition = new Vector3(widthMin + extents.x, pos.y);
            Vector3 offsetVector = new Vector3(offset, 0, 0);
            
            return main.ScreenToWorldPoint(correctedPosition + offsetVector);        
        }

        if (bottomLeft.y <= heightMin)
        {
            Vector3 correctedPosition = new Vector3(pos.x, heightMin + extents.y);
            Vector3 offsetVector = new Vector3(0, offset, 0);
            
            return main.ScreenToWorldPoint(correctedPosition + offsetVector);
        }

        return null;
    }

    #endregion

}
