using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    public GameObject winpanel;
    public GameObject losepanel;

    public string firstPlanetOpenText =
        "What you just opened is called a Planet Folder. These are the life-blood of the company,"+
        " and must be taken care of to increase your Daily Sadisfaction Goal. Check the stats.TXT file "+
        "in each planet for more information on their needs. All areas should be in the green, "+
        "or else satisfaction points will be lost. Add modifiers to planet folders to increase or decrease these stats to get them green! "+
        "A folder has been left on the desktop containing all the modifiers you will need!";
    

    public bool firstPlanetOpen=false;
    public bool firstModOpen=false;
    public bool firstEmailReceived=false;
    
    internal bool collidedTut;
    internal bool webTut;
    internal bool downloadTut;

    /* private void Awake()
     {
         DontDestroyOnLoad(this.gameObject);
     }
 */
    // Start is called before the first frame update
    void Awake()
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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene"))
        {
            StartCoroutine(PlaySoundAndGo());
        }
        else
            SceneManager.LoadScene("MainMenu");
    }

    IEnumerator PlaySoundAndGo()
    {
        SoundManager.main.PlaySoundEffect(SoundEffects.donewithgame);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LoadCreditScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void Loss()
    {
        //play loss sound effect
        SoundManager.main.musicSource.Stop();
        SoundManager.main.PlaySoundEffect(SoundEffects.shutdown);
        
        //play loss visual effects
        losepanel.SetActive(true);
    }

    public void Win()
    {
        
        //
        winpanel.SetActive(true);
        //play sound win
        SoundManager.main.PlaySoundEffect(SoundEffects.daycomplete);
    }
}
