using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSceneManagerScript : MonoBehaviour {

	Text costTextUI; 

	// Use this for initialization
	void Start () {
        Text iterationCountTextUI = GameObject.Find("IterationCount").GetComponent<Text>();
        iterationCountTextUI.text = LevelData.getCurrentIteration().ToString();

        costTextUI = GameObject.Find("Cost").GetComponent<Text>();
		int previousMana = LevelData.GetManaBeforeDiagnose ();
		Debug.Log("mana before diag: " +  previousMana);
		if ((previousMana == -1) || currentlyInDiagnose()) {
			Debug.Log ("set properly");
			LevelData.setCurrentMana (int.Parse (costTextUI.text));
		} else {
			Debug.Log ("reset");
			costTextUI.text = previousMana.ToString ();
			LevelData.setCurrentMana (previousMana);
			LevelData.SetSceneManaBeforeDiagnose (-1, -1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		costTextUI.text = LevelData.getCurrentMana().ToString();

		if (int.Parse(costTextUI.text) == 0) {
			disableAllPotionCollider ();
		}
	}

    void disableAllPotionCollider(){
		GameObject[] potions = GameObject.FindGameObjectsWithTag ("Potion");
		foreach (GameObject gameObject in potions) {
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

	bool currentlyInDiagnose(){
		// diagnose scenes have index 7.8.9
		return (SceneManager.GetActiveScene ().buildIndex > 6);
	}
}
