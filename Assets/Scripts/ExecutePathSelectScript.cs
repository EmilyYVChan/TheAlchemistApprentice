using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExecutePathSelectScript : MonoBehaviour
{
	public List<GameObject> otherInputs;
	public bool isDefault;
	public List<GameObject> pathObjects;
	public int pathIndex;

	private int potionStepCount = 0;

	// Use this for initialization
	void Start ()
	{
		if (!isDefault) {
			// render input semi-transparent if this input is not default
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .4f);
		} 
		else {
			// highlight the associated control flow path if this input is default
			ChangePipeColour(pathObjects,new Color (1f, 1f, 0f, 1f)); // yellow
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnMouseDown()
	{
		// change input opacity
		foreach (GameObject otherInput in otherInputs) {
			otherInput.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .4f);
		}

		this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);

		// un-highlight other control flow path
		foreach (GameObject otherInput in otherInputs){
			ExecutePathSelectScript epss = otherInput.GetComponent<ExecutePathSelectScript> ();
			List<GameObject> otherPathObjects = epss.pathObjects;
			ChangePipeColour(otherPathObjects,new Color (1f, 1f, 1f, 1f)); // white
		}

		// highlight control flow path
		ChangePipeColour(pathObjects,new Color (1f, 1f, 0f, 1f)); // yellow

		// set RunStepBtn with THIS gameObject
		Button runOneStepBtn = GameObject.Find("RunStepBtn").GetComponent<Button>();
		runOneStepBtn.onClick.AddListener (RunOneStep);

		// clear step count whenever a new path is selected
		potionStepCount = 0;
	}

	private void ChangePipeColour(List<GameObject> pathObjects, Color colour){
		foreach (GameObject pathObject in pathObjects){
			ExecutePotionScript potion = pathObject.GetComponent<ExecutePotionScript> ();

			List<GameObject> pipes = potion.pipes;
			foreach (GameObject pipe in pipes) {
				pipe.GetComponent<SpriteRenderer> ().color = colour;
			}
		}
	}

	public void RunOneStep(){
		GameObject pathObject = pathObjects [potionStepCount];
		ExecutePotionScript potion = pathObject.GetComponent<ExecutePotionScript> ();

		// hide original inputs and outputs
		potion.HideAndShowInputsOutputs(false);

		// display actual inputs and outputs

		if (potionStepCount == 1) {
			// output of first potion depends on the pathIndex

			List<GameObject> actualInputs = potion.actualInputs [pathIndex].list;
			foreach (GameObject actualInput in actualInputs) {
				actualInput.SetActive (true);
			}

			List<GameObject> actualOutputs = potion.actualOutputs [pathIndex].list;
			foreach (GameObject actualOutput in actualOutputs) {
				actualOutput.SetActive (true);
			}

		} else {
			// non-first potions only receive one input when executed which can be determined by matching previous output

		}

		potionStepCount ++;
	}
}

