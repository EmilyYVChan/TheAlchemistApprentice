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

    private Button nextStageButton;

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
        SceneManager.UnloadScene(testingSceneName);
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
        nextStageButton = GameObject.Find("NextBtn").GetComponent<Button>();
        nextStageButton.onClick.AddListener(updateNextStageButton);
        resetIterationCount();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void updateNextStageButton()
    {
        Debug.Log("UpdateNextStageButton invoked");
        string btnText = nextStageButton.GetComponentInChildren<Text>().text;
        Debug.Log("btnText = " + btnText);
        if (btnText.Equals("Test"))
        {
            nextStageButton.GetComponentInChildren<Text>().text = "Inspect";
        } else
        {
            nextStageButton.GetComponentInChildren<Text>().text = "Test";
        }
    }
}
