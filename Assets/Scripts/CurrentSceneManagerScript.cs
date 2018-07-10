using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSceneManagerScript : MonoBehaviour {

	Text costTextUI; 

	// Use this for initialization
	void Start () {
        Text iterationCountTextUI = GameObject.Find("IterationCount").GetComponent<Text>();
        iterationCountTextUI.text = LevelData.getCurrentIteration().ToString();

        costTextUI = GameObject.Find("Cost").GetComponent<Text>();
		LevelData.setCurrentCost(int.Parse(costTextUI.text));
	}
	
	// Update is called once per frame
	void Update () {
        costTextUI.text = LevelData.getCurrentCost().ToString();

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
}
