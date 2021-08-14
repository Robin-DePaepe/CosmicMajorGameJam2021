using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserManager : MonoBehaviour
{

    #region properties
    [SerializeField] private GameObject tabsParent;
    [SerializeField] private GameObject tabPrefab;
    [SerializeField] private GameObject addButton;
    [SerializeField] private GameObject defaultPage;

    const int maxTabs = 3;

    List<GameObject> tabs = new List<GameObject>();
    #endregion

    private void Start()
    {
        AddNewTab();
    }

    public void AddNewTab()
    {
        GameObject tab = Instantiate(tabPrefab, tabsParent.transform);

        tabs.Add(tab);
        tab.GetComponent<TabBehaviour>().Page = Instantiate(defaultPage, transform);

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
            tabs.Remove(tab);
            addButton.SetActive(true);

            if (tabs.Count == 0) Destroy(this.gameObject);
            else tabs[0].GetComponent<TabBehaviour>().ShowPage();

        }
    }
}
