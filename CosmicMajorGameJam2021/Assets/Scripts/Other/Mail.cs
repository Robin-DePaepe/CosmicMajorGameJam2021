using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mail : MonoBehaviour
{
    #region variables
    [SerializeField] private Image iconSprite;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI info;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private Sprite openMailIcon;
    [SerializeField] private Sprite bossMailIcon;
    [SerializeField] private Image iconImage;


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

        if (data.mailType == MailData.mailTypes.boss)
        {
            iconImage.sprite = bossMailIcon;
        }
        title.text = data.subject;
        info.text = data.infoSender;
    }

    public void Select()
    {
        if (data.mailType != MailData.mailTypes.boss) iconImage.sprite = openMailIcon;
        MailManager.main.MailSelected(this);
    }
    public void Close()
    {
        MailManager.main.RemoveMail(this);
        Destroy(this.gameObject);
    }
}
public class MailData
{
    #region Variables
    public string subject;
    public string infoSender;
    public string body;
    public string mailArrivalTime;
    public mailTypes mailType;

    public enum mailTypes { regular, boss };
    #endregion

    public MailData(string _title, string _info, string _body, mailTypes _mailType)
    {
        body = _body;
        infoSender = _info;
        subject = _title;
        mailType = _mailType;
    }
}

