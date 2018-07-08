using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour {

    public GameObject canvasJournalObject;
    public GameObject componentViewJournal;
    public GameObject systemViewJournal;
    public GameObject pathsViewJournal;
    public GameObject noComponentsRecordedText;
    public GameObject graph;
    public GameObject systemViewTab;
    public GameObject componentViewTab;
    public GameObject pathsViewTab;

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
        activateComponentView();
        deactivateSystemView();
        deactivatePathsView();
        graph.SetActive(false);
    }

    public void closeJournal()
    {
        canvasJournalObject.SetActive(false);
        graph.SetActive(true);
    }

    public void openComponentsTab()
    {
        activateComponentView();
        deactivateSystemView();
        deactivatePathsView();
    }

    public void openSystemsTab()
    {
        deactivateComponentView();
        activateSystemView();
        deactivatePathsView();
    }

    public void openPathsTab()
    {
        deactivateComponentView();
        deactivateSystemView();
        activatePathsView();
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
            highlightActiveSystemViewButton("SystemViewJournalLevelTutorialBtn");
            systemViewsGameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (level == 1)
        {
            highlightActiveSystemViewButton("SystemViewJournalLevel1Btn");
            systemViewsGameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            highlightActiveSystemViewButton("SystemViewJournalLevel2Btn");
            systemViewsGameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void showPathView(int level)
    {
        //disable all levels' path view
        GameObject pathViewsGameObject = GameObject.Find("PathViews");
        foreach (Transform child in pathViewsGameObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        //enable the desired system view
        if (level == 0)
        {
            highlightActivePathViewButton("PathsViewJournalLevelTutorialBtn");
            pathViewsGameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (level == 1)
        {
            highlightActivePathViewButton("PathsViewJournalLevel1Btn");
            pathViewsGameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            highlightActivePathViewButton("PathsViewJournalLevel2Btn");
            pathViewsGameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void activateComponentView()
    {
        if (JournalData.checkIfHasNotInspectedAnyComponents() == true)
        {
            noComponentsRecordedText.SetActive(true);
            componentViewJournal.SetActive(false);
        }
        else
        {
            noComponentsRecordedText.SetActive(false);
            componentViewJournal.SetActive(true);
        }
        componentViewTab.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        componentViewTab.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

    private void deactivateComponentView()
    {
        componentViewJournal.SetActive(false);
        noComponentsRecordedText.SetActive(false);
        componentViewTab.GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        componentViewTab.GetComponentInChildren<Text>().color = new Color32(120, 120, 120, 255);
    }
    
    private void activateSystemView()
    {
        systemViewJournal.SetActive(true);
        systemViewTab.GetComponent<Image>().color = new Color32(255,255,255,255);
        systemViewTab.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

    private void deactivateSystemView()
    {
        systemViewJournal.SetActive(false);
        systemViewTab.GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        systemViewTab.GetComponentInChildren<Text>().color = new Color32(120, 120, 120, 255);
    }

    private void activatePathsView()
    {
        pathsViewJournal.SetActive(true);
        pathsViewTab.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        pathsViewTab.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

    private void deactivatePathsView()
    {
        pathsViewJournal.SetActive(false);
        pathsViewTab.GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        pathsViewTab.GetComponentInChildren<Text>().color = new Color32(120, 120, 120, 255);
    }

    private void highlightActiveSystemViewButton(string activeButtonName)
    {
        //dim all buttons before highlighting the desired button
        GameObject.Find("SystemViewJournalLevelTutorialBtn").GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        GameObject.Find("SystemViewJournalLevel1Btn").GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        GameObject.Find("SystemViewJournalLevel2Btn").GetComponent<Image>().color = new Color32(120, 120, 120, 255);

        GameObject.Find(activeButtonName).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    private void highlightActivePathViewButton(string activeButtonName)
    {
        //dim all buttons before highlighting the desired button
        GameObject.Find("PathsViewJournalLevelTutorialBtn").GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        GameObject.Find("PathsViewJournalLevel1Btn").GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        GameObject.Find("PathsViewJournalLevel2Btn").GetComponent<Image>().color = new Color32(120, 120, 120, 255);

        GameObject.Find(activeButtonName).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
