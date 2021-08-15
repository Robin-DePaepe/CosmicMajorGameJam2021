using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MailManager : MonoBehaviour
{
    #region Variables
    public static MailManager main;

    private static List<Mail> mails = new List<Mail>();

    static private TextAsset mailList;

    #endregion

    private void Start()
    {
        main = this;

        //StartCoroutine(ScheduleNewMail(10f, new Mail("titel", "robin DP", "wat ben jij goed")));
        if (!mailList)
        {
            mailList = Resources.Load<TextAsset>("EmailChart");
            CSVReader.LoadFromString(mailList.text, Reader);
        }
    }

    public IEnumerator ScheduleNewMail(float time, Mail mail)
    {
        yield return new WaitForSeconds(time);

        AddMail(mail);
    }

    private void AddMail(Mail mail)
    {
        mails.Add(mail);

        //play sound
        /*if (mail.Sender() == "Bossy")
            SoundManager.main.PlaySoundEffect(SoundEffects.bossemail);
        else
            SoundManager.main.PlaySoundEffect(SoundEffects.email);*/
        //fire pop up
        //WindowManager.main.CreatePopUp(new Vector3(1000,-1000,0), "New Mail from " + mail.Sender(),0, 5f);
    }

    private void Reader(int lineIndex, List<string> line)
    {
        Mail mail = new Mail();

        ScheduleNewMail(float.Parse(line[0]), mail);
    }
}
