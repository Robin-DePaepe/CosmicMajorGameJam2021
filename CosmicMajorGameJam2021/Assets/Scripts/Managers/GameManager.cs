using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

   /* private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
*/
    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void Loss()
    {
        //play loss sound effect
        //play loss visual effects
        SceneManager.LoadScene("EndScene");
    }

    public void Win()
    {
        
    }
}
