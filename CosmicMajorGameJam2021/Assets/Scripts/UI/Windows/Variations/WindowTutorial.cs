using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WindowTutorial : WindowConjoined
{
    public string tutorialText;
    public TextMeshProUGUI text;
    public UnityEvent onClose;
    protected override void Start()
    {
        base.Start();
        text.text = tutorialText;
    }

    public override void Close()
    {
        onClose.Invoke();
        Destroy(gameObject);
    }
}
