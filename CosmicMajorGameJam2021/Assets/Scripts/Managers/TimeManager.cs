using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TimeManager : MonoBehaviour
{
    [Header("Time convention")]
    [Tooltip("The multiplying rate at which time moves in game. This means 1 second real time = (rate*time) in seconds in game")]
    public float rate;

    [Header("Work day times")]
    [Tooltip("in seconds")]
    [SerializeField] private GameTime startTime;
    [Tooltip("in seconds")]
    [SerializeField] private GameTime endTime;

    [Header("Mod download")] [SerializeField]
    [Tooltip("Game Time minutes")]
    private float modDownLoadTime = 5;

    [Tooltip("Game Time Hours")] [SerializeField]
    private float mailMin;

    [Tooltip("Game Time Hours")] [SerializeField]
    private float mailMax;
    public GameTime currentTime;
    private GameTime nextSusDecrease=new GameTime();
    public bool timePaused=true;

    [SerializeField] private GameObject mailManager;
    List<Mail> productMails = new List<Mail>();
    int currentMailID = 0;

    public static TimeManager main;

    #region Unity

    private void Awake()
    {
        main = this;
        currentTime = startTime;
        StartCoroutine(Clock());
        StartCoroutine(createProductMails());
        Invoke(nameof(BeginTheDay),Time.deltaTime);
        Instantiate(mailManager, transform);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
        if (currentTime >= nextSusDecrease)//suspicion decay
        {
            SuspicionManager.main.ReduceSuspicion(SuspicionManager.main.suspicionDecay);
            nextSusDecrease = currentTime + new GameTime(ConvertRealTimeToGameTime(SuspicionManager.main.suspicionRate), 0, 0);
            nextSusDecrease.convert();
        }
    }
    
    #endregion

    #region Time functions

    IEnumerator createProductMails()
    {
        while (gameObject.activeSelf)
        {
            GameTime scheduledTime = currentTime + new GameTime(Random.Range(mailMin,mailMax) * 3600, 0, 0);
            while (scheduledTime >= currentTime)
            {
                yield return new WaitForEndOfFrame();
            }
            SentProductEmail();
        }
    }
    
    IEnumerator Clock()
    {
        while (currentTime <= endTime)
        {
            if (!timePaused)
            {
                currentTime.addDeltaTime();
            }
            yield return new WaitForEndOfFrame();
        }
        EndOfDay();
        
    }
    public void ChangePause() => timePaused = !timePaused;
    public void unPauseTime()
    {
        if (rate > 0)
        {
            timePaused = false;
        }
    }
    public float ConvertRealTimeToGameTime(float inLifeTime)//converts real time in seconds to in game time seconds
    {
        return inLifeTime * rate;
    }
    public float ConvertGameTimeToRealTime(float inGameTime)//converts game time in seconds to in real time seconds
    {
        return inGameTime / rate;
    }

    #endregion

    #region Day

    public void BeginTheDay()
    {
        SoundManager.main.PlaySoundEffect(SoundEffects.daystart);
    }

    public void EndOfDay()
    {
        SatisfactionManager.main.CheckSatisfactionCondition();
    }
    
    #endregion

    #region Emails

    public void AddProductEmail(Mail mail)
    {
        bool siteAlreadyIn = false;
        for (int i = 0; i < productMails.Count; i++)
        {
            if (productMails[i].data.siteName == mail.data.siteName)
            {
                siteAlreadyIn = true;
            }
        }

        if (!siteAlreadyIn)
        {
            productMails.Insert(Random.Range(0, productMails.Count), mail);
        }
    }
    private void SentProductEmail()
    {
        if (currentMailID < productMails.Count)
        {
            StartCoroutine(MailManager.ScheduleNewMail(productMails[currentMailID].gameObject));
            ++currentMailID;
        }

    }


    #endregion

    #region Mods

    public void ScheduleModDownLoad(string mod)//time is in game time
    {
        WindowManager.main.CreatePopUp("Notification: " + mod + " modifier downloading",0,3f);
        StartCoroutine(ScheduleAddMod(mod, modDownLoadTime));
    }

    IEnumerator ScheduleAddMod(string mod,float time)
    {
        GameManager.main.checkTutorial(tutNames.download);
        GameTime scheduledTime = currentTime + new GameTime(time * 60, 0, 0);
        while (scheduledTime >= currentTime)
        {
            yield return new WaitForEndOfFrame();
        }
        //play notification sound
        SoundManager.main.PlaySoundEffect(SoundEffects.notice);
        //add modifier to list
        ModManager.main.AddModByName(mod);
        //create pop up to notify player of download
        WindowManager.main.CreatePopUp("Notification: " + mod + " modifier downloaded",0,5f);
    }

    #endregion
}
