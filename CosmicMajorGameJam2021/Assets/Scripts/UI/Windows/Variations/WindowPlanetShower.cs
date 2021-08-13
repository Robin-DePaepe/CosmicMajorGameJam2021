using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowPlanetShower : WindowConjoined
{
    internal Planet planet;
    public Animator animator;
    public Slider slider;
    public override void Minimise()
    {
        base.Minimise();
    }

    protected override void Start()
    {
        base.Start();
        animator.Play(planet.planetName.ToLower());
        slider.minValue = planet.behaviour.getMin();
        slider.maxValue = planet.behaviour.getMax();
        slider.value = planet.behaviour.offsetDistanceToSun;
        slider.onValueChanged.AddListener(delegate(float value){ planet.behaviour.SetDistance(value); });
    }
    public override void Close()
    {
        base.Close();
    }
    void Update()
    {
        if (!planet)
        {
            Destroy(gameObject);
        }
    }
}
