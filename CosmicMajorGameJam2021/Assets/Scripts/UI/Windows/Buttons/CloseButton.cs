using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : WindowButtons
{
    public override void OnButtonClick()
    {
        window.Close();
    }
}
