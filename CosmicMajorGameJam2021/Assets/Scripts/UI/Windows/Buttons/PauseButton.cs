using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{

    public GameObject pausePanel;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        
        image = GetComponent<Image>();
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        TimeManager.main.ChangePause();
        image.color = new Color(image.color.r,image.color.g,image.color.b,pausePanel.activeSelf?1:0 );
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
}
