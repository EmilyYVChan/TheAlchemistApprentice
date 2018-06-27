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

	private int costOfExecutionPerComponent = 1;
	private bool hasBreakpoint = false;

	// Use this for initialization
	public override void Start ()
	{
		// THIS SHOULD BE CALLED FIRST
		// does what PotionScript does
		base.Start ();

		// hides actual inputs and outputs
		if(!LevelData.isPotionExecuted (this.gameObject.name)){
			foreach (ListWrapper listWrapper in actualInputs) {
				List<GameObject> inputRow = listWrapper.list;
				foreach (GameObject gameObject in inputRow) {
					if (!gameObject.tag.Equals ("IO")) {
						gameObject.SetActive (false);
					}
				}                
			}

			foreach (ListWrapper listWrapper in actualOutputs) {
				List<GameObject> outputRow = listWrapper.list;
				foreach (GameObject gameObject in outputRow) {
					if (!gameObject.tag.Equals ("IO")) {
						gameObject.SetActive (false);
					}
				}                
			}
		}
	}

	public override void OnMouseDown()
	{
		// incur costs, should show the expected inputs/outputs of these components
		if (!LevelData.isPotionExecuted(this.gameObject.name))
		{
			hasBreakpoint = true;
			breakpointText.SetActive (true); // shows the "breakpoint" label underneath potion
			LevelData.addCost(costOfExecutionPerComponent);
			LevelData.addExecutedPotion (this.gameObject.name);
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
}

