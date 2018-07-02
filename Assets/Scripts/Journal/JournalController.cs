using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour {

    public GameObject canvasJournalObject;
    public GameObject componentViewJournal;
    public GameObject systemViewJournal;
    public GameObject noComponentsRecordedText;

    // Use this for initialization
    void Start () {
        canvasJournalObject.SetActive(false);
        componentViewJournal.SetActive(false);
        systemViewJournal.SetActive(false);
        noComponentsRecordedText.SetActive(false);
    }
	
    public void openJournal()
    {
        canvasJournalObject.SetActive(true);
        if (JournalData.checkIfHasNotInspectedAnyComponents() == true)
        {
            noComponentsRecordedText.SetActive(true);
            componentViewJournal.SetActive(false);
        } else
        {
            noComponentsRecordedText.SetActive(false);
            componentViewJournal.SetActive(true);
        }
        
        systemViewJournal.SetActive(false);
    }

    public void closeJournal()
    {
        canvasJournalObject.SetActive(false);
    }

    public void openComponentsTab()
    {
        componentViewJournal.SetActive(true);
        systemViewJournal.SetActive(false);
    }

    public void openSystemsTab()
    {
        componentViewJournal.SetActive(false);
        systemViewJournal.SetActive(true);
    }

    public void showSystemView(int level)
    {
        //disable all system views
        GameObject systemViewsGameObject = GameObject.Find("SystemViews");
        foreach (Transform child in systemViewsGameObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        //enable the desired system view
        if (level == 0)
        {
            systemViewsGameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (level == 1)
        {
            systemViewsGameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            systemViewsGameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
