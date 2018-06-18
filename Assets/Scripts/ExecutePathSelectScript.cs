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
	private List<Sprite> previousOutputs = new List<Sprite>();
	private Button runOneStepBtn;

	// Use this for initialization
	void Start ()
	{
		runOneStepBtn = GameObject.Find("RunStepBtn").GetComponent<Button>();
		if (!isDefault) {
			// render input semi-transparent if this input is not default
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .4f);
		} 
		else {
			// highlight the associated control flow path if this input is default
			ChangePipeColour(pathObjects,new Color (1f, 1f, 0f, 1f)); // yellow

			// set RunStepBtn with THIS gameObject
			runOneStepBtn.onClick.AddListener (RunOneStep);
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
		runOneStepBtn.onClick.RemoveAllListeners();
		runOneStepBtn.onClick.AddListener (RunOneStep);

		// clear step count whenever a new path is selected
		potionStepCount = 0;
		runOneStepBtn.interactable = true;
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

		int matchingIndex = -1;
		if (potionStepCount == 0) {
			// output of first potion depends on the pathIndex
			matchingIndex = pathIndex;
		
		} else {
			// non-first potions only receive one input when executed which can be determined by matching previous output
			List<PotionScript.ListWrapper> actualInputs = potion.actualInputs;

			for (int i =0; i < actualInputs.Count; i++){
				
				List<GameObject> actualInput = actualInputs[i].list;
				if (MatchInputOutput (actualInput)) {
					matchingIndex = i; 
					break;
				}
			}
		}

		// hide original inputs and outputs 	//potion.HideAndShowInputsOutputs(false);
		HideOriginalInputOutput(potion, matchingIndex);

		// display actual inputs and outputs
		DisplayActualInputOutput (potion, matchingIndex);

		// increase step count and check if further steps are allowed
		potionStepCount ++;
		if (potionStepCount == pathObjects.Count) {
			runOneStepBtn.interactable = false;
		}
	}

	private void DisplayActualInputOutput(ExecutePotionScript potion, int index){
		previousOutputs.Clear ();

		List<GameObject> actualInputs = potion.actualInputs [index].list;
		foreach (GameObject actualInput in actualInputs) {
			actualInput.SetActive (true);
			//actualInput.GetComponent<SpriteRenderer>().color =  new Color (1f, 1f, 1f, 1f); // reset to non-transparent
		}

		List<GameObject> actualOutputs = potion.actualOutputs [index].list;
		foreach (GameObject actualOutput in actualOutputs) {
			actualOutput.SetActive (true);
			//actualOutput.GetComponent<SpriteRenderer>().color =  new Color (1f, 1f, 1f, 1f); // reset to non-transparent

			// add sprite to previousOutput
			Sprite sprite = actualOutput.GetComponent<SpriteRenderer>().sprite;
			previousOutputs.Add (sprite);
		}
	}

	private void HideOriginalInputOutput(ExecutePotionScript potion, int index){

		List<GameObject> originalInputs = potion.inputs [index].list;
		foreach (GameObject originalInput in originalInputs) {
			originalInput.SetActive (false);
		}

		Debug.Log ("index : " + index);

		List<GameObject> originalOutputs = potion.outputs [index].list;
		foreach (GameObject originalOutput in originalOutputs) {
			Debug.Log ("originalOutput " + originalOutput.name);
			originalOutput.SetActive (false);
		}
	}

	private bool MatchInputOutput(List<GameObject> inputs){

		foreach (GameObject input in inputs) {
			Sprite sprite = input.GetComponent<SpriteRenderer>().sprite;
			if (!previousOutputs.Contains(sprite)){
				return false;
			}
		}
		return true;
	}
}

