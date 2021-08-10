using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowConjoined : Window
{
    public override void Close()
    {
        gameObject.SetActive(false);
    }
}
