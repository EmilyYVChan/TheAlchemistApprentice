using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static bool created = false;
    public static int currentIterationCount;

    private string testingSceneName = "TutorialTestingScene";

    private string inspectStageName = "Inspect";
    private string testStageName = "Test";

    private Button nextStageButton;
    private Text iterationCountTextUI;
    private Text currentStageTextUI;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    public void loadTestingScene()
    {
        SceneManager.LoadScene(testingSceneName, LoadSceneMode.Additive);
    }

    public void unloadTestingScene()
    {
        SceneManager.UnloadSceneAsync(testingSceneName);
    }

    public void incrementIterationCount()
    {
        currentIterationCount++;
    }

    public void resetIterationCount()
    {
        currentIterationCount = 1;
    }

    // Use this for initialization
    void Start()
    {
        resetIterationCount();
        iterationCountTextUI = GameObject.Find("IterationCount").GetComponent<Text>();
        iterationCountTextUI.text = currentIterationCount.ToString();

        currentStageTextUI = GameObject.Find("Stage").GetComponent<Text>();
        currentStageTextUI.text = inspectStageName;

        nextStageButton = GameObject.Find("NextBtn").GetComponent<Button>();
        updateNextStageButtonText(determineNextStage(currentStageTextUI.text));
        nextStageButton.onClick.AddListener(goToNextStage);

        resetIterationCount();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void goToNextStage()
    {
        string oldStage = currentStageTextUI.text;
        string newStage = determineNextStage(oldStage);

        updateIterationCountIfAppropriate(newStage);

        updateTopPanelUI(newStage);

        updateNextStageButtonText(oldStage);
    }

    private void updateTopPanelUI(string newStage)
    {
        currentStageTextUI.text = newStage;
        iterationCountTextUI.text = currentIterationCount.ToString();
    }

    private void updateIterationCountIfAppropriate(string newStage)
    {
        if (newStage.Equals(inspectStageName))
        {
            incrementIterationCount();
        }
    }

    private void updateNextStageButtonText(string newText)
    {
        nextStageButton.GetComponentInChildren<Text>().text = newText;
    }

    private string determineNextStage(string currentStage)
    { 
        return (currentStage.Equals(inspectStageName)) ? testStageName : inspectStageName;
    }
}
