using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExecutePotionScript : PotionScript
{
	public List<ListWrapper> actualInputs;
	public List<ListWrapper> actualOutputs;
	public List<GameObject> pipes;
	public GameObject breakpointText;

	private bool hasBreakpoint = false;

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();

		List<PotionPathIndexPair> executedPotionPathIndexPairs = LevelData.getExecutedPotionPathIndexPairs ();
	
		foreach (PotionPathIndexPair executedPotionPathIndexPair in executedPotionPathIndexPairs){
			if(executedPotionPathIndexPair.getPotionName().Equals(this.gameObject.name)){
				int index = executedPotionPathIndexPair.getIOIndex ();
				List<GameObject> actualInput = actualInputs [index].list;

				foreach (GameObject gb in actualInput) {
					gb.SetActive (true);
				}

				List<GameObject> actualOutput= actualOutputs [index].list;
				foreach (GameObject gb in actualOutput) {
					gb.SetActive (true);
				}

				List<GameObject> input = inputs [index].list;
				foreach (GameObject gb in input) {
					gb.SetActive (false);
				}

				List<GameObject> output = outputs [index].list;
				foreach (GameObject gb in output) {
					gb.SetActive (false);
				}
			}
		}
	}

	public override void OnMouseDown()
	{
		bool potionExecuted = LevelData.isPotionExecuted (new PotionPathIndexPair (this.gameObject.name, LevelData.getCurrentActivePath ())); 
		Debug.Log ("executed? : " + potionExecuted);
		if (ExecutePathSelectScript.PotionIsOnCurrentPath (this.gameObject) && !potionExecuted) {
			// if the potion being clicked on is on the selected control flow path

			int tempCurrentMana;
			if (hasBreakpoint) {
				// plus one because player is trying to clear brkpt on THIS object
				tempCurrentMana = LevelData.getCurrentMana () - GetAllBreakpoints () + 1; 
			} else {
				// minus one extra because player is trying to set brkpt on THIS object 
				tempCurrentMana = LevelData.getCurrentMana () - GetAllBreakpoints () - 1; 
			}

			// potion not executed but enough mana
			if ((tempCurrentMana >= 0)) {
				hasBreakpoint = !hasBreakpoint;
				breakpointText.SetActive (hasBreakpoint);
				Button runOneStepBtn = GameObject.Find ("RunStepBtn").GetComponent<Button> ();
				runOneStepBtn.interactable = (GetAllBreakpoints () != 0);
				if (!runOneStepBtn.interactable) {
					ExecutePathSelectScript.ClearPotionStepCount ();
				}
			} else {
				// potion not executed but not enough mana
				dialogue.SetActive (true);
			} 
		}// potion executed
		else if (potionExecuted) {
			ShowFormula (LevelData.getCurrentActivePath ());
		}
		// show warning diaglogue if the potion selected in not on the current control flow path NOR executed
		else {
			dialogue.SetActive(true);
			foreach (Transform child in dialogue.transform)
			{
				if (child.gameObject.name.Equals ("ManaWarning")) {
					child.gameObject.SetActive (false);
				} 
				else if (child.gameObject.name.Equals ("PathWarning")) {
					child.gameObject.SetActive (true);				
				}
			}

		}
	}

	public void ShowFormula(int currentPathIndex){
		// the correct formula to be displayed should be the one named with potionName_pathIndex
		GameObject formulaWrapper = GameObject.Find("Formula");

		// clear currently showing formula
		foreach (Transform f in formulaWrapper.transform)
		{
				f.gameObject.SetActive (false);
		}

		// show the formula that corresponds to the current path
		if (formulae.Count > 1) {
			foreach (GameObject f in formulae) {
				if (f.name.Equals(this.name+"_"+currentPathIndex)){
					f.SetActive(true);
				}
			}
		}else{
			formulae[0].SetActive(true);				
		}
	}

	public void ClearBreakpoint(){
		hasBreakpoint = false;
	}

	public bool PotionHasBreakpoint(){
		return hasBreakpoint;
	}

	public void HideBreakpointText(){
		breakpointText.SetActive (false);
		Text text = breakpointText.GetComponent<Text> ();
		text.color = new Color (1f, 1f, 1f, 1f); // white
	}

	public void ChangeBreakpointTextColour(){
		Text text = breakpointText.GetComponent<Text> ();
		text.color = new Color (1f, 1f, 0f, 1f); // yellow
	}

	public static int GetAllBreakpoints(){
		GameObject[] potions = GameObject.FindGameObjectsWithTag ("Potion");
		int count = 0;
		foreach (GameObject gameObject in potions) {
			ExecutePotionScript eps = gameObject.GetComponent<ExecutePotionScript> ();
			if (eps.PotionHasBreakpoint()) {
				count++;
			}
		}
		return count;
	}

}

