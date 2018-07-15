using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    private string executeSceneName = "TutorialExecuteScene";
    private string inspectSceneName = "TutorialScene";

    public void loadScene(int sceneNumber)
    {
		if (shouldIncrementIterationCount (sceneNumber)) {
			LevelData.incrementIteration ();

			if (LevelData.getCurrentIteration() == 2)
			{
				TutorialManagerInspect.setIsSecondIteration();
			}
		} else if (shouldResetIterationCount (sceneNumber)) {
			LevelData.resetIterationCount ();
		}
        SceneManager.LoadScene(sceneNumber);
    }

    public void loadTutorialInspectScene()
    {
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

	private bool shouldIncrementIterationCount(int nextSceneNumber) {
		int currentSceneNumber = SceneManager.GetActiveScene ().buildIndex;

		if (isAnExecuteScene(currentSceneNumber) && isSameLevel(nextSceneNumber, currentSceneNumber)) {
			return true;
		} else {
			return false;
		}
	}

	private bool isAnExecuteScene(int sceneNumber) {
		if (sceneNumber == 2 || sceneNumber == 4 || sceneNumber == 6) {
			return true;
		} else {
			return false;
		}
	}

	private bool isSameLevel(int inspectSceneNumber, int executeSceneNumber) {
		if (inspectSceneNumber == (executeSceneNumber - 1)) {
			return true;
		} else {
			return false;
		}
	}

	private bool shouldResetIterationCount(int nextSceneNumber) {
		int currentSceneNumber = SceneManager.GetActiveScene ().buildIndex;

		if (isAnExecuteScene (currentSceneNumber) && !isSameLevel (nextSceneNumber, currentSceneNumber)) {
			return true;
		} else {
			return false;
		}
	}
}