using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionManager : MonoBehaviour
{
    [Tooltip("At what value of suspicion does the player lose?")]
    [SerializeField] private int suspicionLossValue;
    
    
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
        suspicion += addition;
        CheckForLossCondition();
    }

    public void ReduceSuspicion(int reduction)
    {
        suspicion -= reduction;
    }

    public void CheckForLossCondition()
    {
        if (suspicion >= suspicionLossValue)
        {
            //insert what happens when we lose here
            GameManager.main.Loss();
        }
    }
}
