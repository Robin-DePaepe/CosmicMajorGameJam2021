using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowserManager : MonoBehaviour
{

    #region properties
    [SerializeField] private GameObject tabsParent;
    [SerializeField] private GameObject tabPrefab;
    [SerializeField] private GameObject addButton;
    [SerializeField] private GameObject searchDefaultPage;
    [SerializeField] private GameObject pageParent;
    const int maxTabs = 3;

    List<GameObject> tabs = new List<GameObject>();

    //pages
    [SerializeField] private GameObject defaultWebPage;

    static private TextAsset pagesList;
    // key: link string, value: destination page of the link
    static Dictionary<string,DownloadBehaviour.DownloadType> pageLookupTable = new Dictionary<string, DownloadBehaviour.DownloadType>();
    #endregion

    private void Start()
    {
        if
            (!pagesList)
        {
            pagesList = Resources.Load<TextAsset>("Mods");
            CSVReader.LoadFromString(pagesList.text, Reader);
        }
        AddNewTab();
    }

    private void Reader(int lineIndex, List<string> line)
    {
        if (lineIndex > 0)
        {
            string site = line[8];
            DownloadBehaviour.DownloadType type = DownloadBehaviour.DownloadType.scam;

            switch (line[9])
            {
                case "malWare":
                    type = DownloadBehaviour.DownloadType.malware;
                    break;
                case "scam":
                    type = DownloadBehaviour.DownloadType.scam;
                    break;
                case "mod":
                    type = DownloadBehaviour.DownloadType.mod;
                    break;
                case "blackHole":
                    type = DownloadBehaviour.DownloadType.blackHole;
                    break;
                default:
                    break;
            }
            if (!pageLookupTable.ContainsKey(site.ToLower())) pageLookupTable.Add(site.ToLower(),type);
        }
    }

    private void AddNewTab()
    {
        GameObject defaultPage = Instantiate(searchDefaultPage, pageParent.transform);
        AddTab(defaultPage, "New Page");
    }

    public void AddNewSite(string siteName)
    {
        siteName = siteName.ToLower();
        if (pageLookupTable.ContainsKey(siteName))
        {
            if (tabs.Count == maxTabs) tabs[0].GetComponent<TabBehaviour>().Close();

            GameObject page = Instantiate(defaultWebPage, pageParent.transform);
            page.GetComponentInChildren<DownloadBehaviour>().SiteAdress = siteName;
            page.GetComponentInChildren<DownloadBehaviour>().SetDownloadType( pageLookupTable[siteName]);

            AddTab(page, siteName);
            if (!GameManager.main.webTut)
            {
                WindowManager.main.createTutorial(WindowManager.main.webTut);
                GameManager.main.webTut = true;
            }
        }
    }
    private void AddTab(GameObject page, string pageName)
    {
        GameObject tab = Instantiate(tabPrefab, tabsParent.transform);
        tabs.Add(tab);
        tab.GetComponent<TabBehaviour>().Page = page;
        tab.GetComponent<TabBehaviour>().Title = pageName;

        int addButtonIndex = tabs.Count;

        addButton.transform.SetSiblingIndex(addButtonIndex);
        if (addButtonIndex == maxTabs) addButton.SetActive(false);
    }

    public void DisableTabPages()
    {
        foreach (GameObject tab in tabs)
        {
            tab.GetComponent<TabBehaviour>().DisablePage();
        }
    }

    public void RemovePage(GameObject tab)
    {
        if (tabs.Contains(tab))
        {
            addButton.SetActive(true);
            addButton.transform.SetSiblingIndex(tabs.Count);

            tabs.Remove(tab);

            if (tabs.Count == 0) Destroy(this.gameObject);
            else tabs[0].GetComponent<TabBehaviour>().ShowPage();

        }
    }

}
