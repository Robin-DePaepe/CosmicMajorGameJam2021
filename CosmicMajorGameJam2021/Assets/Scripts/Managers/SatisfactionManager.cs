using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactionManager : MonoBehaviour
{
    [Tooltip("How much satisfaction does the player need to win the level?")]
    [SerializeField]private int requiredSatisfaction=100;
    public int satisfaction=0;

    public static SatisfactionManager main;

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
        threshold = requiredSatisfaction /20;
        SetBarSprite();
    }

    // Update is called once per frame
    void Update()
    {
        SetBarSprite();
    }
    public void ResetSatisfaction()
    {
        satisfaction = 0;
    }

    public void AddSatisfaction(int addition)
    {
        satisfaction =Mathf.Max(0,Mathf.Min(satisfaction+addition,100));
        SetBarSprite();
        
    }
    public void ReduceSatisfaction(int reduction)
    {
        satisfaction =Mathf.Max(satisfaction-reduction,0);
        SetBarSprite();
    }
    public void CheckSatisfactionCondition()
    {
        if (satisfaction >= requiredSatisfaction)
        {
            GameManager.main.Win();
            
        }
        else
        {
            //insert what happens when we lose here
            GameManager.main.Loss();
        }
    }

    public void SetBarSprite()
    {
        
        bar.sprite = barSprites[Mathf.Max(0,(int)(satisfaction / Mathf.Max(1, threshold))-1)];
    }
}
