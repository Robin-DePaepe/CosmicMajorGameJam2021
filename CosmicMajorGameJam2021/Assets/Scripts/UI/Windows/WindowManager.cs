using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindowManager : MonoBehaviour
{
    //Ensure there is a camera attached to the Canvas Component
    //Ensure that the camera has the tag "MainCamera"

    #region Components
    
    internal Canvas canvasComponent;
    public static WindowManager main;
    [Header("Window Types")]
    public GameObject normal;
    public GameObject hazardous;
    public GameObject conjoined;
    public GameObject planetStat;
    public GameObject popUpTemplate;
    public GameObject tutorialTemplate;
    [Header("Window Parents")]
    public GameObject windowParent;
    public GameObject onTopParent;
    public GameObject popUpParent;
    
    [Header("Other Components")]
    public GameObject minimisedWindows;
    public GameObject minimisedTemplate;
    public GameObject canvas;
    public RectTransform taskBar; //for getting the position of the taskbar to set the edges of the screen
    internal Camera mainCamera;
    
    #endregion

    #region Variables
    [Header("Variables")]
    public float offset; //offset between the window's border and edge of screen
    internal IDictionary<GameObject, WindowEntry> windows;
    public string startTut;
    public string collideTut;
    public string webTut;
    public string downloadTut;
    #endregion

    #region Unity Functions

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        windows = new Dictionary<GameObject, WindowEntry>();
        
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        canvasComponent = canvas.GetComponent<Canvas>();
        createTutorial(startTut);
    }

    #endregion

    #region Specific Windows

    public void CreatePopUp(string popUpText,float timeTillPop,float timeLasted)
    {
        GameObject window = CreateWindow(Vector3.zero, popUpTemplate, parent: popUpParent);
        WindowsConjoinedPopUp script = (WindowsConjoinedPopUp) windows[window].script;
        script.SetPop(popUpText, timeTillPop,timeLasted);
    }

    public void createTutorial(string text)
    {
        GameObject window = CreateWindow(Vector3.zero, tutorialTemplate, true);
        WindowTutorial script = (WindowTutorial) windows[window].script;
        script.tutorialText = text;

        TimeManager.main.timePaused = true;
        script.onClose.AddListener(TimeManager.main.unPauseTime);
    }
    #endregion

    #region Window Functions
    
    public GameObject CreateWindow(Vector3 position, GameObject windowTemplate, bool onTop = false , Sprite Icon = null, bool check = true, GameObject parent = null) 
    {
        //creates a window with the location and name specified, and returns it (if needed)
        //the location is then checked for remaining within the screen
        //the name is only for displaying at the taskbar, this can be changed to an icon
        if (!parent)
        {
            parent = windowParent;
        }
        if(onTop)
        {
            parent = onTopParent;
        }
        GameObject window = Instantiate(windowTemplate, position, Quaternion.identity, parent.transform);

        
        RectTransform windowRect = window.GetComponent<RectTransform>();
        
        windowRect.position = position;

        if (check)
        {
            CorrectPosition(windowRect);
        }

        window.transform.SetAsLastSibling();
        
        Window script = window.GetComponent<Window>();
        script.manager = this;
        script.Icon = Icon;
        windows.Add(window,new WindowEntry(windowRect, script));
        
        return window;
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

    public void CorrectPosition(RectTransform rect)
    {
        
        for (int i = 0; i < 2; i++)
        {
            Vector3? within = withinScreen(rect);

            if (within != null)
            {
                Vector3 correctedPosition = (Vector3) within;
                correctedPosition.z = 0;
                rect.position = correctedPosition;
            }
        }
        
    }

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
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(mainCamera, rect.position);
        //get size and extent of screen in Screen coordinates (pixels)

        Vector2 topRight = extents + pos;
        Vector2 bottomLeft = pos - extents;
        //sets bottom left and top right for comparing with the screen edges

        if (topRight.y >= heightMax)
        {
            Vector3 correctedPosition = new Vector3(pos.x, heightMax - extents.y); //extents are added or subtracted to get the centred position
            Vector3 offsetVector = new Vector3(0, -offset, 0); //offset added to prevent window from being locked
            
            return mainCamera.ScreenToWorldPoint(correctedPosition + offsetVector); //returns world point
        }

        if (topRight.x >= widthMax)
        {
            Vector3 correctedPosition = new Vector3(widthMax - extents.x, pos.y);
            Vector3 offsetVector = new Vector3(-offset, 0, 0);
            
            return mainCamera.ScreenToWorldPoint(correctedPosition + offsetVector);        
        }

        if (bottomLeft.x <= widthMin)
        {
            Vector3 correctedPosition = new Vector3(widthMin + extents.x, pos.y);
            Vector3 offsetVector = new Vector3(offset, 0, 0);
            
            return mainCamera.ScreenToWorldPoint(correctedPosition + offsetVector);        
        }

        if (bottomLeft.y <= heightMin)
        {
            Vector3 correctedPosition = new Vector3(pos.x, heightMin + extents.y);
            Vector3 offsetVector = new Vector3(0, offset, 0);
            
            return mainCamera.ScreenToWorldPoint(correctedPosition + offsetVector);
        }

        return null;
    }

    #endregion
    
}

public class WindowEntry
{
    internal RectTransform rect;
    internal Window script;

    public WindowEntry(RectTransform rect, Window script)
    {
        this.rect = rect;
        this.script = script;
    }
}