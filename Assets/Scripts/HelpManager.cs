using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour {

    public GameObject overviewViewTab;
    public GameObject studyViewTab;
    public GameObject operateViewTab;
    public GameObject overviewViewJournal;
    public GameObject studyViewJournal;
    public GameObject operateViewJournal;
    public GameObject canvasHelp;
    public GameObject graph;

    // Use this for initialization
    void Start () {
        canvasHelp.SetActive(false);
        overviewViewJournal.SetActive(false);
        studyViewJournal.SetActive(false);
        operateViewJournal.SetActive(false);
    }

    // Update is called once per frame
    public void openHelp()
    {
        canvasHelp.SetActive(true);
        activateOverviewView();
        deactivateStudyView();
        deactivateOperateView();
        graph.SetActive(false);
    }
    public void closeHelp()
    {
        canvasHelp.SetActive(false);
        graph.SetActive(true);
    }

    public void openOverviewTab()
    {
        activateOverviewView();
        deactivateStudyView();
        deactivateOperateView();
    }

    public void openStudyTab()
    {
        deactivateOverviewView();
        activateStudyView();
        deactivateOperateView();
    }

    public void openOperateTab()
    {
        deactivateOverviewView();
        deactivateStudyView();
        activateOperateView();
    }

    private void activateOverviewView()
    {
        overviewViewJournal.SetActive(true);
        overviewViewTab.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        overviewViewTab.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

    private void deactivateOverviewView()
    {
        overviewViewJournal.SetActive(false);
        overviewViewTab.GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        overviewViewTab.GetComponentInChildren<Text>().color = new Color32(120, 120, 120, 255);
    }

    private void activateStudyView()
    {
        studyViewJournal.SetActive(true);
        studyViewTab.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        studyViewTab.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

    private void deactivateStudyView()
    {
        studyViewJournal.SetActive(false);
        studyViewTab.GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        studyViewTab.GetComponentInChildren<Text>().color = new Color32(120, 120, 120, 255);
    }

    private void activateOperateView()
    {
        operateViewJournal.SetActive(true);
        operateViewTab.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        operateViewTab.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

    private void deactivateOperateView()
    {
        operateViewJournal.SetActive(false);
        operateViewTab.GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        operateViewTab.GetComponentInChildren<Text>().color = new Color32(120, 120, 120, 255);
    }
}
