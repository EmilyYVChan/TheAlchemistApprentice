using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    private string executeSceneName = "TutorialExecuteScene";
    private string inspectSceneName = "TutorialScene";

    public void loadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void loadTutorialInspectScene()
    {
        SceneManager.LoadScene(inspectSceneName);
        LevelData.incrementIteration();
    }

    public void loadTutorialExecuteScene()
    {
        SceneManager.LoadScene(executeSceneName);
    }
}