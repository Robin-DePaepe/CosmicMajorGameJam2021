using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionManager : MonoBehaviour
{
    [Tooltip("At what value of suspicion does the player lose?")]
    [SerializeField] private int suspicionLossValue=100;
    [Header("Suspicion decrease variables")]
    [Tooltip("Amount of decrease per suspicion rate decrease")]
    [SerializeField] public int suspicionDecay;
    [Tooltip("Time for suspicion decrease to happen(in real time seconds)")]
    [SerializeField] public int suspicionRate;
    
    
    public int suspicion;

    public static SuspicionManager main;
    
    [Header("Bar")]
    public Image bar;
    public Sprite[] barSprites;
    private int threshold;

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        threshold = suspicionLossValue / (barSprites.Length-1);
        SetBarSprite();
    }

    // Update is called once per frame
    void Update()
    {
        SetBarSprite();
    }

    public void AddSuspicion(int addition)
    {
        suspicion = Mathf.Min(suspicion + addition, 100);
        CheckForEmailCondition();
        SetBarSprite();
    }

    public void ReduceSuspicion(int reduction)
    {
        suspicion =Mathf.Max(suspicion-reduction,0);
        SetBarSprite();
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
            //MailManager.main.ScheduleNewMail(0f, new Mail());
        }
        if (suspicion >= 50)
        {
            //send 50% sus mail
            //MailManager.main.ScheduleNewMail(0f, new Mail());

        }
        if (suspicion >= 25)
        {
            //send 25% sus mail
            //MailManager.main.ScheduleNewMail(0f, new Mail());
        }
        
        
    }
    
    public void SetBarSprite()
    {
        bar.sprite = barSprites[Mathf.Max(0,(int)(suspicion /Mathf.Min(1, threshold))-1)];
    }
}
