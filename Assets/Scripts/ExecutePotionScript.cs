using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecutePotionScript : PotionScript
{
	public List<ListWrapper> actualInputs;
	public List<ListWrapper> actualOutputs;
	public List<GameObject> pipes;
	private int costOfExecutionPerComponent = 1;

	// Use this for initialization
	public override void Start ()
	{
		// hides actual inputs and outputs
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

		// does what PotionScript does
		// !! If actual input = original input; parent Start() will reset the input to visibile
		// therefore base.Start() must be called after hiding actual inputs and outputs
		base.Start ();
	}

	public override void OnMouseDown()
	{
		// incur costs, should show the expected inputs/outputs of these components
		if (!LevelData.isPotionInspected(this.gameObject.name))
		{
			LevelData.addCost(costOfExecutionPerComponent);
			LevelData.addInspectedPotion (this.gameObject.name);
			//Debug.Log("entered !isAlreadyInspected ");
			//string currentCostString = Regex.Match(costTextUI.text, @"\d+").Value;
			//int oldCost = System.Int32.Parse(currentCostString);
			//Debug.Log("oldCost = " + oldCost);
			//int newCost = oldCost + (costOfInspectionPerFormula * inputs.Count);
			//Debug.Log("newCost = " + newCost);
			//string newCostString = Regex.Replace(costTextUI.text, @"\d", newCost.ToString());
			//costTextUI.text = newCostString;
		}
	}
}

