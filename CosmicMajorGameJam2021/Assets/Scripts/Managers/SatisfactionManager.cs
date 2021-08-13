using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionManager : MonoBehaviour
{
    [Tooltip("How much satisfaction does the player need to win the level?")]
    [SerializeField]private int requiredSatisfaction;
    private int satisfaction;

    public static SatisfactionManager main;

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetSatisfaction()
    {
        satisfaction = 0;
    }

    public void AddSatisfaction(int addition)
    {
        satisfaction =Mathf.Min(satisfaction+addition,100);
        
    }
    public void ReduceSatisfaction(int reduction)
    {
        satisfaction =Mathf.Max(satisfaction-reduction,0);
    }
    public void CheckSatisfactionCondition()
    {
        if (satisfaction >= requiredSatisfaction)
        {
            //pass on to next day
            
        }
        else
        {
            //insert what happens when we lose here
            GameManager.main.Loss();
        }
    }
}
