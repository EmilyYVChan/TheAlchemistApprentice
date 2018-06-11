using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static bool created = false;
    public static int currentIteration;

    private string testingSceneName = "TutorialTestingScene";

    private string inspectStageName = "Inspect";
    private string testStageName = "Test";

    private Button nextStageButton;
    private Text iterationCountText;
    private Text currentStageText;

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
        currentIteration++;
    }

    public void resetIterationCount()
    {
        currentIteration = 1;
    }

    // Use this for initialization
    void Start()
    {
        iterationCountText = GameObject.Find("IterationCount").GetComponent<Text>();
        currentStageText = GameObject.Find("Stage").GetComponent<Text>();

        nextStageButton = GameObject.Find("NextBtn").GetComponent<Button>();
        updateNextStageButtonText(determineNextStage(currentStageText.text));
        nextStageButton.onClick.AddListener(goToNextStage);

        resetIterationCount();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void goToNextStage()
    {
        string oldStage = currentStageText.text;
        string newStage = determineNextStage(oldStage);

        updateIterationCountIfAppropriate(newStage);

        updateTopPanelUI(newStage);

        updateNextStageButtonText(oldStage);
    }

    private void updateTopPanelUI(string newStage)
    {
        currentStageText.text = newStage;
        iterationCountText.text = currentIteration.ToString();
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
