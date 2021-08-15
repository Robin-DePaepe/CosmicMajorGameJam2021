using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DownloadBehaviour : MonoBehaviour
{
    #region variables
    private bool hasBought;
    private string site;
    private Sprite[] sprites = null;

    private DownloadType type;
    public enum DownloadType { mod, blackHole, malware, scam };

    [SerializeField] private Image webImage;
    #endregion

    public string SiteAdress
    {
        get { return site; }
        set
        {
            site = value;
            sprites = Resources.LoadAll<Sprite>($"SitePages/{site}");
            webImage.sprite = sprites[0];
        }
    }

    public void SetDownloadType(DownloadType _type)
    {
        type = _type;
    }
    public void OnDownload()
    {
        if (hasBought) return;

        if (sprites != null)
        {
            webImage.sprite = sprites[2];
        }
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

    public void OnHover()
    {
        if (hasBought || sprites == null) return;
        webImage.sprite = sprites[1];
    }

    public void OnHoverExit()
    {
        if (hasBought || sprites == null) return;
        webImage.sprite = sprites[0];
    }
}
