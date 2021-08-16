using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrowserBehaviour : MonoBehaviour
{
    #region Properties
    [SerializeField] private TMP_InputField searchBar;
    [SerializeField] private GameObject linkTemplate;
    [SerializeField] private GameObject linkParent;

    private BrowserManager browserManager;

    //history
    static List<string> history = new List<string>();

    const int linkCapasity = 7;
    #endregion

    private void Awake()
    {
        if(!GameManager.main.browserTut)
        {
            WindowManager.main.createTutorial(WindowManager.main.browserTut);
            GameManager.main.browserTut = true;
        }
        browserManager = transform.root.GetComponentInChildren<BrowserManager>();

        if (!searchBar) Debug.LogError("No search bar given for the browser.");

        if (history.Count > 0)
        {
            for (int i = history.Count - 1; i >= 0; i--)
            {
                GameObject link = Instantiate(linkTemplate, linkParent.transform);
                link.GetComponentInChildren<TextMeshProUGUI>().text = history[i];
                link.GetComponent<LinkBehaviour>().LinkAdress = history[i];
            }
        }
    }
    public void Search()
    {
        string inputText = searchBar.text;

        if (inputText.Length == 0) return;

        browserManager.AddNewSite(inputText);

        //update the browsers history
        if (!history.Contains(inputText))
        {
            history.Add(inputText);

            GameObject link = Instantiate(linkTemplate, linkParent.transform);

            link.GetComponent<LinkBehaviour>().LinkAdress = inputText;
            link.GetComponentInChildren<TextMeshProUGUI>().text = inputText;
            link.transform.SetSiblingIndex(1); //this will set is as the first link (just below the title which is index 0)

            if (history.Count == linkCapasity)
            {
                Destroy(linkParent.transform.GetChild(linkParent.transform.childCount - 1).gameObject);
                history.RemoveAt(0);
            }
        }
    }
    public void Clear()
    {
        searchBar.text = "";
    }
}
