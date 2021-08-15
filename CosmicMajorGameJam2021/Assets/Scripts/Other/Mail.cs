using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    #region Variables
    private string title;
    private string infoSender;
    private string body;
    #endregion

    public Mail(string _title, string _info, string _body)
    {
        body = _body;
        infoSender = _info;
        title = _title;
    }

}
