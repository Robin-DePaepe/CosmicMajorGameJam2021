using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAnimatorExample : MonoBehaviour
{
    public Animator animator;

    public string[] clips;

    private int index = 0;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index = (index + 1) % clips.Length;
            animator.Play(clips[index]);
        } else if (Input.GetMouseButtonDown(1))
        {
            index = index - 1;
            if (index < 0)
                index = clips.Length - 1;
            animator.Play(clips[index]);
        }
    }
}
