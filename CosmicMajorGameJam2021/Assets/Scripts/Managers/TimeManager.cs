using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time convention")]
    [Tooltip("The multiplying rate at which time moves in game. This means 1 second real time = (rate*time) in seconds in game")]
    [SerializeField] private float rate;

    [Header("Work day times")]
    [Tooltip("in seconds")]
    [SerializeField] private float startTime;
    [Tooltip("in seconds")]
    [SerializeField] private float endTime;

    private float totalTimeOfWorkDay;

    public static TimeManager main;

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        totalTimeOfWorkDay = ConvertGameTimeToRealTime(Mathf.Abs(endTime - startTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float ConvertRealTimeToGameTime(float inLifeTime)//converts real time in seconds to in game time
    {
        return inLifeTime * rate;
    }
    public float ConvertGameTimeToRealTime(float inGameTime)//converts game time in seconds to in real  time
    {
        return inGameTime / rate;
    }

}
