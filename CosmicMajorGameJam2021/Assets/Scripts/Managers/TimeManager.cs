﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameTime //time object for in game events, to be set in game standard time
{
    public int hours;
    public int minutes;
    public int seconds;

    public void convert()
    {
        if (seconds >= 60)
        {
            int minAdd = seconds % 60;
            minutes += minAdd;
            seconds -=minAdd*60;
        }
        if (minutes >= 60)
        {
            int hourAdd = minutes % 60;
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

    public GameTime(int s, int m = 0, int h = 0)
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

    private float totalTimeOfWorkDay;
    private GameTime currentTime;
    
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Clock()
    {
        yield return new WaitForSeconds(1f);
        currentTime += new GameTime((int)ConvertRealTimeToGameTime(1), 0,0 );
    }

    

    public float ConvertRealTimeToGameTime(float inLifeTime)//converts real time in seconds to in game time seconds
    {
        return inLifeTime * rate;
    }
    public float ConvertGameTimeToRealTime(float inGameTime)//converts game time in seconds to in real time seconds
    {
        return inGameTime / rate;
    }

}
