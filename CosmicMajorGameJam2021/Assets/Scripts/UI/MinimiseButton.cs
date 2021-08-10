using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimiseButton : WindowButtons
{
    
    public override void OnButtonClick()
    {
        window.Minimise();
    }
}
