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

    private Dictionary<int, GameObject> suspicionMails = new Dictionary<int, GameObject>();
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
        threshold = suspicionLossValue / (barSprites.Length);
        SetBarSprite();
    }

    // Update is called once per frame
    void Update()
    {
        SetBarSprite();
    }

    public void AddSuspicionMail(int sus, GameObject mail)
    {
        suspicionMails.Add(sus, mail);
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
            GameManager.main.Loss();
        }

        List<int> sentMails = new List<int>(); 
        foreach (var mail in suspicionMails)
        {
            if(suspicion >= mail.Key)
            {
               StartCoroutine( MailManager.ScheduleNewMail(mail.Value));
                sentMails.Add(mail.Key);
            }
        }

        foreach (int mailID in sentMails)
        { 
            suspicionMails.Remove(mailID);
        }
    }
    
    public void SetBarSprite()
    {
        int snum = (Mathf.Max(suspicion,1) / Mathf.Max(1, threshold));
        snum = Mathf.Clamp(snum, 0, barSprites.Length-1);

        bar.sprite = barSprites[snum];
        if (snum == barSprites.Length - 1)
        {
            GameManager.main.Loss();
        }
    }
}
