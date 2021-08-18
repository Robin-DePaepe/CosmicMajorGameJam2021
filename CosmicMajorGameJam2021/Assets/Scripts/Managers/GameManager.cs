using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public enum tutNames
{
    start,
    collide,
    web,
    download,
    blackHole,
    mail,
    browser,
    modFileOpen,
    modOpen,
    planetOpen
}
public class GameManager : MonoBehaviour
{
    public static GameManager main;
    public GameObject winpanel;
    public GameObject losepanel;
    
    #region Tutorials
    
    [SerializeField] List<Tutorial> tutorialList;
    Dictionary<tutNames, Tutorial> tutorials;

    public void checkTutorial(tutNames tut)
    {
        tutorials[tut].Check();
    }
    #endregion
    
    void Awake()
    {
        main = this;

        tutorials = new Dictionary<tutNames, Tutorial>();
        for (int i = 0; i < tutorialList.Count; i++)
        {
            tutorials.Add(tutorialList[i].name, tutorialList[i]);
        }
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
        else SceneManager.LoadScene("MainMenu");
    }

    IEnumerator PlaySoundAndGo()
    {
        SoundManager.main.PlaySoundEffect(SoundEffects.donewithgame);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
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
        winpanel.SetActive(true);
        //play sound win
        SoundManager.main.PlaySoundEffect(SoundEffects.daycomplete);
    }

}

[Serializable]
public class Tutorial
{
    public tutNames name;
    public string text;
    bool done;
    public void Check()
    {
        if (!done)
        {
            WindowManager.main.createTutorial(text);
            done = true;
        }
    }
}
