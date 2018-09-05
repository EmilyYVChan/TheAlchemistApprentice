using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	private string executeSceneName = "TutorialExecuteScene";
	private string inspectSceneName = "TutorialScene";

	public virtual void Start () {
		changeButtonColour ();
	}

	public void loadScene(int sceneNumber)
	{
		SceneManager.LoadScene(sceneNumber);
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

	private void changeButtonColour(){
		string currentStageName = LevelData.GetCurrentStage ();
		if (currentStageName != null) {
			
			Image img = GameObject.Find (currentStageName).GetComponent<Image>();
			img.color = new Color (0.87f,1f,0.22f,1f);

			Button btn = GameObject.Find (currentStageName).GetComponent<Button> ();
			btn.interactable = false;
		}
	}

	public void setCurrentStage(string name){
		LevelData.SetCurrentStage (name);
	}
}