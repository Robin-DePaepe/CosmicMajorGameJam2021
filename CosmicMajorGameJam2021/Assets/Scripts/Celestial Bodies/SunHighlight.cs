using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunHighlight : MonoBehaviour
{
    public Sprite[] spriteCycle;
    private int index;
    public float coolDown;
    private float timeSinceLast;
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(waitThenSwap());
    }
    
    IEnumerator waitThenSwap()
    {
        while (gameObject.activeSelf)
        {

            if (!TimeManager.main.timePaused)
            {
                if (timeSinceLast >= coolDown)
                {
                    sprite.sprite = spriteCycle[index];

                    index++;
                    if (index >= spriteCycle.Length)
                    {
                        index = 0;
                    }
                    timeSinceLast = 0;
                }
                
                timeSinceLast += Time.deltaTime;
            }
            
            yield return new WaitForEndOfFrame();

        }
    }
}
