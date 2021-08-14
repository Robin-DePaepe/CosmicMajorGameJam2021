using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBehaviour : MonoBehaviour
{
    #region Variables
    private GameObject page;
    private BrowserManager browserManager;

    public GameObject Page
    {
        get { return page; }
        set {
            page = value; 
        }
    }

    [SerializeField] private Text title;

    public string Title
    { set { title.text = value; } }
    #endregion

    private void Awake()
    {
        browserManager = transform.root.GetComponentInChildren<BrowserManager>();
    }
    public void Close()
    {
        browserManager.RemovePage(this.gameObject);
        Destroy(page);
        Destroy(this.gameObject);
    }

    public void ShowPage()
    {
        page.SetActive(true);
    }

     public void DisablePages()
    {
        browserManager.DisableTabPages();
    }

    public void DisablePage()
    {
        page.SetActive(false);
    }
}
