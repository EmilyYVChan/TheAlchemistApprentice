using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiagnosePotionScript : ExecutePotionScript {
	public bool isFault;
	public GameObject canvasResult;
	Text costTextUI;

	// Use this for initialization
	public override void Start (){
		costTextUI = GameObject.Find("Cost").GetComponent<Text>();
		base.Start ();
	}

	void Update () {
		costTextUI.text = LevelData.getCurrentMana().ToString();

		if (int.Parse(costTextUI.text) == 0) {
			disableAllPotionCollider ();
		}
	}

	public override void OnMouseDown()
	{
		if (isFault) {
			canvasResult.SetActive(true);
	
			int totalCost = LevelData.GetTotalCost ();

			Text mana = GameObject.Find ("Mana").GetComponent<Text>();
			mana.text = totalCost.ToString();

			Text iteration = GameObject.Find ("Iteration").GetComponent<Text>();
			iteration.text = LevelData.getCurrentIteration ().ToString();

			ResultBoundary rb = canvasResult.GetComponent<ResultBoundary> ();
			int lowerBound = rb.lowerBoundary;
			int upperBound = rb.upperBoundary;

			if (totalCost <= lowerBound) {
				GameObject.Find ("OneStar").SetActive (false);
				GameObject.Find ("TwoStars").SetActive (false);
			} else if ((lowerBound < totalCost) && (totalCost <= upperBound)) {
				GameObject.Find ("OneStar").SetActive (false);
				GameObject.Find ("ThreeStars").SetActive (false);
			} else {
				// totalCost > upperBoudn
				GameObject.Find ("ThreeStars").SetActive (false);
				GameObject.Find ("TwoStars").SetActive (false);
			}

		} else {
			
			LevelData.addCost(int.Parse(costTextUI.text) * -1);
			dialogue.SetActive (true);
		}
	}

	private void disableAllPotionCollider(){
		GameObject[] potions = GameObject.FindGameObjectsWithTag ("Potion");
		foreach (GameObject gameObject in potions) {
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
}
