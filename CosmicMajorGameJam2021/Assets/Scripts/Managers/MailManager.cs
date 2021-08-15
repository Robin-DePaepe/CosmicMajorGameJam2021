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
        yield return new WaitForSeconds(time);

        AddMail(mail);
    }

    private void AddMail(GameObject mail)
    {
        Mail mailScript = mail.GetComponent<Mail>();


        WindowManager.main.CreatePopUp( "New mail from: " + mailScript.MailData.infoSender, 0f, 4f);

        //set to latest new mail in the hierachy
        mail.transform.SetSiblingIndex(mailSummaryList.transform.childCount);

        mail.SetActive(true);

        mailScript.SetArrivalTime();
        mails.Add(mailScript);

        Canvas.ForceUpdateCanvases();

        //play sound
        /*if (mail.Sender() == "Bossy")
            SoundManager.main.PlaySoundEffect(SoundEffects.bossemail);
        else
            SoundManager.main.PlaySoundEffect(SoundEffects.email);*/
        //fire pop up
        //WindowManager.main.CreatePopUp(new Vector3(1000,-1000,0), "New Mail from " + mail.Sender(),0, 5f);
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
        MailData.type mailType = MailData.type.regular;

        switch (line[1])
        {
            case "regular":
                break;
            case "boss": mailType = MailData.type.boss;
                break;
            default:
                break;
        }

        GameObject mail = Instantiate(mailSummaryTemplate, mailSummaryList.transform);
        mail.GetComponent<Mail>().SetData(new MailData(line[3], line[2], line[4], mailType));
        mail.SetActive(false);

       StartCoroutine(ScheduleNewMail(float.Parse(line[0]), mail));
    }
}
