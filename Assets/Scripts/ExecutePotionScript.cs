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
		// incur costs, should show the expected inputs/outputs of these components
		if (!LevelData.isPotionExecuted(new PotionPathIndexPair(this.gameObject.name, LevelData.getCurrentActivePath())))
		{
			Debug.Log ("not executed!!");
			hasBreakpoint = true;
			breakpointText.SetActive (true); // shows the "breakpoint" label underneath potion
			Button runOneStepBtn = GameObject.Find("RunStepBtn").GetComponent<Button>();
			runOneStepBtn.interactable = true;
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
	}
}

