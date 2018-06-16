using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    private static bool created = false;
    private static int currentIterationCount;
    private static int currentCost;

    private string executeSceneName = "TutorialExecuteScene";
    private string inspectSceneName = "TutorialScene";

    void Awake()
    {
        if (!created)
        {
            Debug.Log("entered !created");
            DontDestroyOnLoad(this.gameObject);
            created = true;
            resetIterationCount();
            resetCost();
        }
    }

    public void loadExecuteScene()
    {
        SceneManager.LoadScene(executeSceneName, LoadSceneMode.Single);
        updateTopPanelUI();
    }

    public void loadInspectScene()
    {
        currentIterationCount++;
        SceneManager.LoadScene(inspectSceneName, LoadSceneMode.Single);
        updateTopPanelUI();
    }

    public void resetIterationCount()
    {
        currentIterationCount = 1;
    }

    private void resetCost()
    {
        currentCost = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    private void updateTopPanelUI()
    {
        Text iterationCountTextUI = GameObject.Find("IterationCount").GetComponent<Text>();
        iterationCountTextUI.text = currentIterationCount.ToString();

        Text costTextUI = GameObject.Find("Cost").GetComponent<Text>();
        costTextUI.text = currentCost.ToString();
    }
}
