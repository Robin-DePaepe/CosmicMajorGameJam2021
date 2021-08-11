using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private Camera main;

    private void Start()
    {
        main = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
