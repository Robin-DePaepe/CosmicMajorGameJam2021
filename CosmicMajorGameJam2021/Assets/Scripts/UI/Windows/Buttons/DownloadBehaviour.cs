﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DownloadBehaviour : MonoBehaviour
{
    #region variables

    private string site;
    private DownloadType type;

  public  enum DownloadType { mod, blackHole, malware, scam};
    public string SiteAdress
    {
        get { return site; }
        set { site = value; }
    }

    public void SetDownloadType(DownloadType _type)
    {
        type = _type;
    }
    #endregion
    public void OnDownload()
    {
        switch (type)
        {
            case DownloadType.mod:
        ModManager.main.AddModBySite(site);
                break;
            case DownloadType.blackHole:
                PlanetManager.main.createBlackHole();
                break;
            case DownloadType.malware:
                PlanetManager.main.CorruptRandom();
                break;
            case DownloadType.scam: //nothing happens.. you got scammed
                break;
            default:
                break;
        }
    }


}
