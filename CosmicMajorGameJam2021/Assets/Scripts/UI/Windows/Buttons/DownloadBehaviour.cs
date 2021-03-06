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
        
        hasBought = true;
        if (sprites != null)
        {
            webImage.sprite = sprites[2];
        }
        int suspicion = 15;
        switch (type)
        {
            case DownloadType.mod:
                ModManager.main.AddModBySite(site);
                suspicion = 0;
                break;
            case DownloadType.blackHole:
                PlanetManager.main.createBlackHole();
                SoundManager.main.PlaySoundEffect(SoundEffects.error);
                WindowManager.main.CreatePopUp("Looks like you got scammed. A black hole was created", 0f, 3f);
                break;
            case DownloadType.malware:
                PlanetManager.main.CorruptRandom();
                SoundManager.main.PlaySoundEffect(SoundEffects.error);
                WindowManager.main.CreatePopUp("Looks like you got scammed. A planet was corrupted", 0f, 3f);
                break;
            case DownloadType.scam:
                SoundManager.main.PlaySoundEffect(SoundEffects.error);
                WindowManager.main.CreatePopUp("Looks like you got scammed. No mods were downloaded.", 0f, 3f);
                break;
        }
        SuspicionManager.main.AddSuspicion(suspicion);
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
