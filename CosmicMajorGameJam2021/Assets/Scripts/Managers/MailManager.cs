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

    private static Dictionary<uint, GameObject> mails = new Dictionary<uint, GameObject>();
    private static uint mailId = 0;

    static private TextAsset mailList;

    Mail currentSelectedMail = null;

    [SerializeField] private GameObject mailTempHold;
    [SerializeField] private GameObject mailSummaryTemplate;
    [SerializeField] private GameObject mailSummaryList;

    //mail displayable info
    [SerializeField] private TextMeshProUGUI subject;
    [SerializeField] private TextMeshProUGUI senderInfo;
    [SerializeField] private TextMeshProUGUI body;

    private bool applicationQuiting = false;
    #endregion

    private void Start()
    {
        main = this;

        if (!mailList)
        {
            mailList = Resources.Load<TextAsset>("EmailChart");
            CSVReader.LoadFromString(mailList.text, Reader);
        }

        foreach (var mail in mails)
        {
            AddMailVisually(mail.Value);
        }
    }

    public void OpenMailWebSite()
    {
        if (currentSelectedMail == null || currentSelectedMail.MailData.siteName == "") return;

        GameObject shortcut = GameObject.FindGameObjectWithTag("InternetShortcut");
        
        if(shortcut != null)
        {
            Shortcut shortcutScript = shortcut.GetComponent<Shortcut>();
            shortcutScript.clickedOnce = true;
            shortcutScript.OnPointerDown(null);
            
            BrowserManager browser = shortcutScript.window.GetComponent<BrowserManager>();
            browser.AddNewSite(currentSelectedMail.MailData.siteName);
        }
    }

    static public IEnumerator ScheduleNewMail(GameObject mail, float gameTimeHours = 0f)
    {
        GameTime scheduledTime = TimeManager.main.currentTime + new GameTime(gameTimeHours * 3600,0,0);

        while (scheduledTime >= TimeManager.main.currentTime)
        {
            yield return new WaitForEndOfFrame();
        }

        AddMail(mail);
    }

    private void AddMailVisually(GameObject mail)
    {
        if (!main) return;

        //set parent
        mail.transform.SetParent(mailSummaryList.transform);
        mail.SetActive(true);

        //set to latest new mail in the hierachy
        mail.transform.SetSiblingIndex(mailSummaryList.transform.childCount);

        mail.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        Canvas.ForceUpdateCanvases();
    }
    private static void AddMail(GameObject mail)
    {
        Mail mailScript = mail.GetComponent<Mail>();

        mailScript.SetArrivalTime();
        mailScript.SetMailId(mailId);

        mails.Add(mailId, mail);
        ++mailId;

        //play sound
        if (mailScript.MailData.mailType == MailData.mailTypes.boss)
            SoundManager.main.PlaySoundEffect(SoundEffects.bossemail);
        else
            SoundManager.main.PlaySoundEffect(SoundEffects.email);

        //fire pop up
        WindowManager.main.CreatePopUp("New mail from: " + mailScript.MailData.infoSender, 0f, 4f);

        if (main) main.AddMailVisually(mail);
    }

    static public void RemoveMail(uint id)
    {
        mails.Remove(id);
    }

    public void MailSelected(Mail mail)
    {
        //give tutorial
        GameManager.main.checkTutorial(tutNames.mail);

        //set the data of the mail view
        currentSelectedMail = mail;
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
        MailData.mailTypes mailType = MailData.mailTypes.productAd;
        string siteName = "";

        switch (line[0])
        {
            case "":
                return;
            case "Information":
                mailType = MailData.mailTypes.information;
                break;
            case "Work Addition Email":
                mailType = MailData.mailTypes.boss;
                break;
            case "Warning Email":
                mailType = MailData.mailTypes.warning;
                break;
            default:
                if (line[0].Contains("Product Ad"))
                {
                    mailType = MailData.mailTypes.productAd;
                    siteName = line[1];
                }
                else return;
                break;
        }

        GameObject mail = Instantiate(mailSummaryTemplate, mailSummaryList.transform);
        Mail mailScript = mail.GetComponent<Mail>();
        mailScript.SetData(new MailData(line[3], line[2], line[4], mailType, siteName));
        mail.SetActive(false);

        if (mailType == MailData.mailTypes.information || mailType == MailData.mailTypes.boss)
            StartCoroutine(ScheduleNewMail(mail, float.Parse(line[1])));
        else
        {
            switch (mailType)
            {
                case MailData.mailTypes.productAd:
                    TimeManager.main.AddProductEmail(mailScript);
                    break;
                case MailData.mailTypes.warning:
                    SuspicionManager.main.AddSuspicionMail((int)float.Parse(line[1]), mail);
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        if (!applicationQuiting)
        {
            foreach (var mail in mails)
            {
                if (mail.Value && !applicationQuiting)
                {
                    mail.Value.transform.SetParent(null);
                    mail.Value.gameObject.SetActive(false);
                }
            }
        }

    }
    private void OnApplicationQuit()
    {
        applicationQuiting = true;
    }
}
