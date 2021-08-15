using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    #region Variables
    public static MailManager main;

    private static List<Mail> mails = new List<Mail>();

    static private TextAsset mailList;

    [SerializeField] private GameObject mailSummaryList;
    [SerializeField] private GameObject mailSummaryTemplate;
    #endregion

    private void Start()
    {
        main = this;

        StartCoroutine(ScheduleNewMail(10f, new MailData("titel", "robin DP", "wat ben jij goed",MailData.type.boss)));
        if (!mailList)
        {
            mailList = Resources.Load<TextAsset>("EmailChart");
            CSVReader.LoadFromString(mailList.text, Reader);
        }
    }

    public IEnumerator ScheduleNewMail(float time, MailData mail)
    {
        yield return new WaitForSeconds(time);

        AddMail(mail);
    }

    private void AddMail(MailData mailData)
    {
     GameObject mail = Instantiate(mailSummaryTemplate, mailSummaryList.transform);
        mail.GetComponent<Mail>().SetData(mailData);
   //     mails.Add(mail);

        //play sound
        //fire pop up
    }

    private void Reader(int lineIndex, List<string> line)
    {
        //Mail mail = new Mail(line[3], line[3], line[4]);

        //ScheduleNewMail(float.Parse(line[0]), mail);
    }
}
