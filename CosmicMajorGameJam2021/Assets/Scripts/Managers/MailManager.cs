using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MailManager : MonoBehaviour
{
    #region Variables
    public static MailManager main;

    private static List<Mail> mails = new List<Mail>();

    static private TextAsset mailList;

    [SerializeField] private GameObject mailSummaryTemplate;
    [SerializeField] private GameObject mailSummaryList;

    //mail displayable info
    [SerializeField] private TextMeshProUGUI subject;
    [SerializeField] private TextMeshProUGUI senderInfo;
    [SerializeField] private TextMeshProUGUI body;
    #endregion

    private void Start()
    {
        main = this;

        if (!mailList)
        {
            mailList = Resources.Load<TextAsset>("EmailChart");
            CSVReader.LoadFromString(mailList.text, Reader);
        }
    }

    public IEnumerator ScheduleNewMail(float time, GameObject mail)
    {
        float timeWaited=TimeManager.main.ConvertGameTimeToRealTime(time*3600);

        while (timeWaited < time)
        {
            if (!TimeManager.main.timePaused)
            {
                timeWaited += Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
        }

        AddMail(mail);
    }

    private void AddMail(GameObject mail)
    {
        Mail mailScript = mail.GetComponent<Mail>();

        //set to latest new mail in the hierachy
        mail.transform.SetSiblingIndex(mailSummaryList.transform.childCount);

        mail.SetActive(true);

        mailScript.SetArrivalTime();
        mails.Add(mailScript);

        Canvas.ForceUpdateCanvases();

        //play sound
        if (mailScript.MailData.mailType == MailData.mailTypes.boss)
            SoundManager.main.PlaySoundEffect(SoundEffects.bossemail);
        else
            SoundManager.main.PlaySoundEffect(SoundEffects.email);

        //fire pop up
        WindowManager.main.CreatePopUp("New mail from: " + mailScript.MailData.infoSender, 0f, 4f);
    }

    public void RemoveMail(Mail mail)
    {
        mails.Remove(mail);
    }

    public void MailSelected(Mail mail)
    {
        subject.text = mail.MailData.subject;
        senderInfo.text = mail.MailData.infoSender;
        body.text = mail.MailData.body;
        StartCoroutine(enableToggle(body.gameObject));
    }

    IEnumerator enableToggle(GameObject objectToToggle)
    {
        objectToToggle.SetActive(false);
        yield return new WaitForEndOfFrame();
        objectToToggle.SetActive(true);
    }
    
    private void Reader(int lineIndex, List<string> line)
    {
        MailData.mailTypes mailType = MailData.mailTypes.regular;

        switch (line[2])
        {
            case "Bossy":
                mailType = MailData.mailTypes.boss;
                break;
        }
        GameObject mail = Instantiate(mailSummaryTemplate, mailSummaryList.transform);
        mail.GetComponent<Mail>().SetData(new MailData(line[3], line[2], line[4], mailType));
        mail.SetActive(false);

       StartCoroutine(ScheduleNewMail(float.Parse(line[0]), mail));
    }
}
