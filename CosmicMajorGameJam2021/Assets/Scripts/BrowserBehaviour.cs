using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowserBehaviour : MonoBehaviour
{
    #region Properties
    [SerializeField] private InputField searchBar;
    [SerializeField] private GameObject linkTemplate;
    [SerializeField] private GameObject linkParent;

    // key: link string, value: destination page of the link
    static Dictionary<string, GameObject> lookupTable = new Dictionary<string, GameObject>();

    //history
    static List<string> history = new List<string>();

    const int linkCapasity = 10;
    #endregion

    private void Awake()
    {
        if (!searchBar) Debug.LogError("No search bar given for the browser.");
      
        if (history.Count > 0)
        {
            for (int i = history.Count -1; i >=0; i--)
            {
                GameObject link = Instantiate(linkTemplate, linkParent.transform);
                link.GetComponentInChildren<Text>().text = history[i];
            }            
        }
    }
    public void Search()
    {
        string inputText = searchBar.text;

        if (inputText.Length > 0 && lookupTable.ContainsKey(inputText))
        {
            //code to open the new page
        }
            history.Add(inputText);

        GameObject link = Instantiate(linkTemplate, linkParent.transform);
        link.GetComponentInChildren<Text>().text = inputText;
        link.transform.SetSiblingIndex(1); //this will set is as the first link (just below the title which is index 0)

        if (history.Count == linkCapasity)
        {
            Destroy(linkParent.transform.GetChild(linkParent.transform.childCount - 1).gameObject);
            history.RemoveAt(0);
        }
    }

    public void Clear()
    {
        searchBar.text = "";
    }
}
