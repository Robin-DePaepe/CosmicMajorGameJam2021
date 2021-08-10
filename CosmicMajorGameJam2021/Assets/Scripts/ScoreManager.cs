using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int score;
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

    public static void AddScore(int addition)
    {
        score += addition;
    }

    public static void ReduceScore(int reduction)
    {
        score -= reduction;
    }
}
