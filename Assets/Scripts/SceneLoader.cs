using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

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

	public void clearLevelDataUponLoadingNextLevel(){
		LevelData.ClearLevelData ();
	}

	public void storeManaAndSceneBeforeDiagnose(int sceneToLoad){
		SceneManager.LoadScene (sceneToLoad);
		Text costTextUI = GameObject.Find("Cost").GetComponent<Text>();
		LevelData.SetSceneManaBeforeDiagnose( SceneManager.GetActiveScene().buildIndex, int.Parse(costTextUI.text));
	}

	public void loadManaAndSceneBeforeDiagnose(){
		SceneManager.LoadScene (LevelData.GetSceneBeforeDiagnose());
	}

	public void incrementIteration() {
		LevelData.incrementIteration ();

		if (LevelData.getCurrentIteration() == 2)
		{
			TutorialManagerInspect.setIsSecondIteration();
		}
	}
}