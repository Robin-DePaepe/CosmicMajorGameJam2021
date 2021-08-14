using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionManager : MonoBehaviour
{
    [Tooltip("At what value of suspicion does the player lose?")]
    [SerializeField] private int suspicionLossValue=100;
    [Header("Suspicion decrease variables")]
    [Tooltip("Amount of decrease per suspicion rate decrease")]
    [SerializeField] public int suspicionDecay;
    [Tooltip("Time for suspicion decrease to happen(in real time seconds)")]
    [SerializeField] public int suspicionRate;
    
    
    private int suspicion;

    public static SuspicionManager main;

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

    public void AddSuspicion(int addition)
    {
        suspicion = Mathf.Min(suspicion + addition, 100);
        CheckForEmailCondition();
    }

    public void ReduceSuspicion(int reduction)
    {
        suspicion =Mathf.Max(suspicion-reduction,0);
    }

    public void CheckForEmailCondition()
    {
        if (suspicion >= suspicionLossValue)//results in losing the game
        {
            //insert what happens when we lose here
            GameManager.main.Loss();
        }
        if (suspicion >= 75)
        {
            
            //send 75% sus mail
        }
        if (suspicion >= 50)
        {
            //send 50% sus mail
            
        }
        if (suspicion >= 25)
        {
            //send 25% sus mail
            
        }
        
        
    }
}
