using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DownloadBehaviour : MonoBehaviour
{
    #region variables

    private string site;

    public string SiteAdress
    {
        get { return site; }
        set { site = value; }
    }

    #endregion
    public void OnDownload()
    {
        ModManager.main.AddModBySite(site);
    }


}
