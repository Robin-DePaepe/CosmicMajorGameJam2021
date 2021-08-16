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
        "You just opened a Planet Folder. These must be taken care of to increase your Daily Satisfaction Goal. " + 
        " Check the stats file in each planet for more information. Your stat should be in the green or the satisfaction points will be lost. "+ 
        "Add modifiers to planet folders to increase or decrease these stats! " +
        "A folder has been left on the desktop containing all the modifiers you need!";

    public string firstModOpenText = "Click a Modifier to check out what changes it does to a Planet Folder";
    public string firstModFileOpenText = "Drag and drop the modifier into the planet file to change its stats. You can drag them back into the modifier file too";
    
    public bool firstPlanetOpen=false;
    public bool firstModOpen=false;
    public bool firstModFileOpen=false;
    
    public bool firstEmailReceived=false;
    
    internal bool collidedTut;
    internal bool webTut;
    internal bool downloadTut;

    internal bool blackHoleTut;
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
