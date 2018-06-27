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

			// disable the collider box for potions that are not objects on THIS path
			ToggleBreakpointAndColliderOnPotions ();

			LevelData.setCurrentActivePath (pathIndex);
		}
		runOneStepBtn.interactable = false; // initialise to false because player cannot run until at least a breakpoint is set
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnMouseDown()
	{
		// update current active path
		LevelData.setCurrentActivePath(pathIndex);

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

		//=======================================================================================

		// clear step count whenever a new path is selected
		potionStepCount = 0;
		ToggleBreakpointAndColliderOnPotions ();

		// set RunStepBtn with THIS gameObject
		runOneStepBtn.onClick.RemoveAllListeners();
		runOneStepBtn.onClick.AddListener (RunOneStep);
		runOneStepBtn.interactable = false; // initialise to false because player cannot run until at least a breakpoint is set

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

		// increase step count and check if further steps are allowed
		potionStepCount ++;
		PopulatePreviousOutputs (potion, matchingIndex);

		if (potion.PotionHasBreakpoint ()) {
			// hide original inputs and outputs 	
			HideOriginalInputOutput (potion, matchingIndex);

			// display actual inputs and outputs
			DisplayActualInputOutput (potion, matchingIndex);
			potion.ClearBreakpoint ();

			// add cost for displaying actual inputs and outputs
			LevelData.addCost(1);
			LevelData.addExecutedPotion (new PotionPathIndexPair(potion.gameObject.name, pathIndex, matchingIndex));

			if (!StillHasBreakpoints ()) {
				// all potions do not have anymore breakpoints
				runOneStepBtn.interactable = false;
				potionStepCount = 0;
			}
			return;
		} else {
			RunOneStep ();
		}
    }

	private void ToggleBreakpointAndColliderOnPotions(){

		// clear breakpoints on all potions
		GameObject[] potions = GameObject.FindGameObjectsWithTag("Potion");
		foreach (GameObject gameObject in potions) {
			ExecutePotionScript ep = gameObject.GetComponent<ExecutePotionScript> ();
			ep.ClearBreakpoint ();
			ep.HideBreakpointText();
		}

		// disable box collider on potions not involved in this path
		// enable box collider on potions involved in this path (in case it has been disabled previously)
		foreach (GameObject potion in potions){
			if (!pathObjects.Contains (potion)) {
				potion.GetComponent<BoxCollider2D> ().enabled = false;
			} else {
				potion.GetComponent<BoxCollider2D> ().enabled = true;
			}
		}
	}

	private void DisplayActualInputOutput(ExecutePotionScript potion, int index){

		List<GameObject> actualInputs = potion.actualInputs [index].list;
		foreach (GameObject actualInput in actualInputs) {
			actualInput.SetActive (true);
			//actualInput.GetComponent<SpriteRenderer>().color =  new Color (1f, 1f, 1f, 1f); // reset to non-transparent
		}

		List<GameObject> actualOutputs = potion.actualOutputs [index].list;
		foreach (GameObject actualOutput in actualOutputs) {
			actualOutput.SetActive (true);
			//actualOutput.GetComponent<SpriteRenderer>().color =  new Color (1f, 1f, 1f, 1f); // reset to non-transparent
		}
	}

	private void PopulatePreviousOutputs(ExecutePotionScript potion, int index){
		previousOutputs.Clear ();
		List<GameObject> actualOutputs = potion.actualOutputs [index].list;
		foreach (GameObject actualOutput in actualOutputs) {
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

	private bool StillHasBreakpoints(){
		foreach (GameObject gameObject in pathObjects) {
			
			ExecutePotionScript eps = gameObject.GetComponent<ExecutePotionScript> ();
			if (eps.PotionHasBreakpoint ()) {
				return true;
			}
		}
		return false;
	}
}

