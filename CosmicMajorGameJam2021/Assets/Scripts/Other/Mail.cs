using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mail : MonoBehaviour
{
    #region variables
    [SerializeField] private Image iconSprite;
    [SerializeField] private Text title;
    [SerializeField] private Text info;
    [SerializeField] private Text time;

    private MailData data;
    #endregion

    public MailData MailData { get { return data; } }
    public void SetArrivalTime()
    {
        string hours = TimeManager.main.currentTime.hours.ToString();
        if (hours.Length < 2)
        {
            hours = "0" + hours;
        }

        string minutes = TimeManager.main.currentTime.minutes.ToString();
        if (minutes.Length < 2)
        {
            minutes = "0" + minutes;
        }

        time.text = hours + ":" + minutes;
    }
    public void SetData(MailData _data)
    {
        data = _data;

        title.text = data.subject;
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

    public void Select()
    {
        MailManager.main.MailSelected(this);
    }
    public void Close()
    {
        MailManager.main.RemoveMail(this);
        Destroy(this.gameObject);
    }
}
public class MailData : MonoBehaviour
{
    #region Variables
    public string subject;
    public string infoSender;
    public string body;
    public string mailArrivalTime;
    public type typeMail;
    public enum type { regular, boss };
    #endregion

    public MailData(string _title, string _info, string _body, type _typeMail = type.regular)
    {
        body = _body;
        infoSender = _info;
        subject = _title;
        typeMail = _typeMail;
    }
}

