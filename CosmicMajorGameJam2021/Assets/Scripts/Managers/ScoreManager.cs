using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;

    public static ScoreManager main;

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScoreInDebug()
    {
        Debug.Log(score);
    }

    public void AddScore(int addition)
    {
        score += addition;
    }

    public void ReduceScore(int reduction)
    {
        score -= reduction;
    }
}
