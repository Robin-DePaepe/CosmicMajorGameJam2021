﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameTime //time object for in game events, to be set in game standard time
{
    public int hours;
    public int minutes;
    public float seconds;

    public void convert()
    {
        if (seconds >= 60)
        {
            int minAdd = Mathf.RoundToInt(seconds) / 60;
            minutes += minAdd;
            seconds -=minAdd*60;
        }
        if (minutes >= 60)
        {
            int hourAdd = minutes / 60;
            hours += hourAdd;
            minutes -=hourAdd*60;
        }
        
    }
    public GameTime()
    {
        seconds = 0;
        hours = 0;
        minutes = 0;
    }

    public GameTime(float s, int m = 0, int h = 0)
    {
        seconds = s;
        hours = h;
        minutes = m;
    }

    public static GameTime operator +(GameTime gt1, GameTime gt2)
    {
        GameTime result=new GameTime(gt1.seconds + gt2.seconds, gt1.minutes + gt2.minutes, gt1.hours + gt2.hours);
        result.convert();
        return result;

    }

    public static GameTime operator -(GameTime gt1,GameTime gt2)=> new GameTime(Mathf.Abs(gt1.seconds - gt2.seconds), Mathf.Abs(gt1.minutes - gt2.minutes), 
        Mathf.Abs(gt1.hours - gt2.hours));

    public static bool operator >=(GameTime gt1, GameTime gt2)
    {
        if (gt1 != null && gt2 != null)
        {
            return (gt1.hours >= gt2.hours) && (gt1.minutes >= gt2.minutes) && (gt1.seconds >= gt2.seconds);
            
        }

        return false;
    }
    
    public static bool operator<=(GameTime gt1, GameTime gt2)
    {
        if (gt1 != null && gt2 != null)
        {
            return  (gt1.hours <= gt2.hours) && (gt1.minutes <= gt2.minutes) && (gt1.seconds <= gt2.seconds);
            
        }
        return false;

    }

};

public class TimeManager : MonoBehaviour
{
    [Header("Time convention")]
    [Tooltip("The multiplying rate at which time moves in game. This means 1 second real time = (rate*time) in seconds in game")]
    [SerializeField] private float rate;

    [Header("Work day times")]
    [Tooltip("in seconds")]
    [SerializeField] private GameTime startTime;
    [Tooltip("in seconds")]
    [SerializeField] private GameTime endTime;

    [Header("Pop up")] 
    [SerializeField]private GameObject popUp;

    [SerializeField] private RectTransform popUpPos;
    private float totalTimeOfWorkDay;
    private GameTime currentTime;

    private bool timePaused=true;
    public bool debug;
    
    public static TimeManager main;

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalTimeOfWorkDay = ConvertGameTimeToRealTime((endTime - startTime).seconds);
        currentTime = startTime;
        timePaused = false;
        StartCoroutine(Clock());
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            LogTime();
        }
    }

    void LogTime()
    {
        if (timePaused)
        {
            Debug.Log("paused");
        }
        else
        {
            Debug.Log(currentTime.hours + " " + currentTime.minutes + " " + currentTime.seconds);
        }
    }
    
    IEnumerator Clock()
    {
        while (!timePaused)
        {
            currentTime += new GameTime(ConvertRealTimeToGameTime(Time.deltaTime), 0,0 );
            if (currentTime >= endTime)
            {
                timePaused = true;
            }
            yield return new WaitForEndOfFrame();
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

    public void BeginTheDay()
    {
        //play music
        //play begin day sound effects
        
    }

    public void EndOfDay()
    {
        SatisfactionManager.main.CheckSatisfactionCondition();
    }

    public void ScheduleModDownLoad(/*the modifier to add, the time taken to add it*/)
    {
        //convert the time, from real time seconds to in-game hours
        //invoke a add to modifier function, send the modifier as argument
        
    }
    
    public void SetPopUpDetails(string notificationText,float timeForPop, float timeToLast)//time is given in real time
    {
       GameObject PO= Instantiate(popUp, popUpPos);
       PO.GetComponent<WindowsConjoinedPopUp>().SetPop(notificationText, timeForPop,timeToLast);

    }
}
