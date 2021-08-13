using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Planet"))
        {
            PlanetManager.main.destroyPlanet(other.gameObject);
        }
    }
}
