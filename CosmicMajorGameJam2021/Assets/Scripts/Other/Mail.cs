using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mail : MonoBehaviour
{
    #region variables
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private Text title;
    [SerializeField] private Text info;

    private MailData data;
    #endregion

    [System.Obsolete]
    public Mail()
    {
        iconSprite = GetComponentInChildren<Image>().sprite;

        title = transform.FindChild("Title").GetComponent<Text>();
        info = transform.FindChild("Info").GetComponent<Text>();
    }
    public void SetData(MailData _data)
    {
        data = _data;

        title.text = data.title;
        info.text = data.infoSender;

        switch (data.typeMail)
        {
            case MailData.type.regular:
                //      iconSprite = ...
                break;
            case MailData.type.boss:
                break;
            default:
                break;
        }
    }
}

public class MailData : MonoBehaviour
{
    #region Variables
    public string title;
    public string infoSender;
    public string body;
    public type typeMail;
    public enum type { regular, boss };
    #endregion

    public MailData(string _title, string _info, string _body, type _typeMail = type.regular)
    {
        body = _body;
        infoSender = _info;
        title = _title;
        typeMail = _typeMail;
    }
}

