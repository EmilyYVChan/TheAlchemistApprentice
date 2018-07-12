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
		Debug.Log ("load tutorial inspect?");
        SceneManager.LoadScene(inspectSceneName);
        LevelData.incrementIteration();

        if (LevelData.getCurrentIteration() == 2)
        {
            TutorialManagerInspect.setIsSecondIteration();
        }
    }

    public void loadTutorialExecuteScene()
    {
        SceneManager.LoadScene(executeSceneName);
    }
}