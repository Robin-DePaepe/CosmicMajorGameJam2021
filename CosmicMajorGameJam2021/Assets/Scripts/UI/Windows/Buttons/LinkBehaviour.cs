using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkBehaviour : MonoBehaviour
{
    #region variables
    private string linkAdress;

    public string LinkAdress
    {
        get { return linkAdress; }
        set { linkAdress = value; }
    }
    #endregion

    public void SearchOnLink()
    {
        transform.root.GetComponentInChildren<BrowserManager>().AddNewSite(linkAdress);
    }

}
