using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExecutePathSelectScript : MonoBehaviour
{
	public List<GameObject> otherInputs;
	public List<GameObject> otherNeighbourInputs;
	public bool isActive;
	public List<PotionScript.ListWrapper> pathObjects;
	public List<int> pathIndex;

	private int potionStepCount = 0;
	private List<List<Sprite>> previousOutputs = new List<List<Sprite>>();
	private Button runOneStepBtn;
	private List<GameObject> currentPathObjects;
	private int currentPathIndex;

	// Use this for initialization
	void Start ()
	{
		// decide on current path index
		currentPathIndex =  GetCurrentPathIndex();
		int index = pathIndex.IndexOf (currentPathIndex);
		currentPathObjects = pathObjects [index].list;

		//===================================================

		runOneStepBtn = GameObject.Find("RunStepBtn").GetComponent<Button>();
		if (!isActive) {
			// render input semi-transparent if this input is not default
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .4f);
		} 
		else {
			// add initial inputs to previousOutputs list to facilitate IO matching
			AddActiveIOToPreviousOutputs();

			// highlight the associated control flow path if this input is default
			ChangePipeColour(currentPathObjects,new Color (1f, 1f, 0f, 1f)); // yellow

			// set RunStepBtn with THIS gameObject
			runOneStepBtn.onClick.RemoveAllListeners();
			runOneStepBtn.onClick.AddListener (RunOneStep);

			// disable the collider box for potions that are not objects on THIS path
			ToggleBreakpointAndColliderOnPotions ();

			LevelData.setCurrentActivePath (currentPathIndex);
		}
		runOneStepBtn.interactable = false; // initialise to false because player cannot run until at least a breakpoint is set
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnMouseDown()
	{
		// set this input to be active, and other neighbour inputs as inactive
		isActive = true;
		ToggleActiveFlagOnOtherInputs ();

		currentPathIndex =  GetCurrentPathIndex();
		int index = pathIndex.IndexOf (currentPathIndex);
		currentPathObjects = pathObjects [index].list;
		//===================================================

		// update current active path
		LevelData.setCurrentActivePath(currentPathIndex);

		// change input opacity
		foreach (GameObject otherInput in otherNeighbourInputs) {
			otherInput.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .4f);
		}

		this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);

		// un-highlight other control flow path
		foreach (GameObject otherInput in otherNeighbourInputs){
			ExecutePathSelectScript epss = otherInput.GetComponent<ExecutePathSelectScript> ();
			List<PotionScript.ListWrapper> otherPathObjects = epss.pathObjects;
			foreach (PotionScript.ListWrapper otherPathObject in otherPathObjects) {
				ChangePipeColour(otherPathObject.list,new Color (1f, 1f, 1f, 1f)); // white
			}
		}

		// highlight control flow path
		ChangePipeColour(currentPathObjects,new Color (1f, 1f, 0f, 1f)); // yellow

		//=======================================================================================

		// clear step count whenever a new path is selected
		potionStepCount = 0;
		ToggleBreakpointAndColliderOnPotions ();

		// set RunStepBtn with THIS gameObject
		runOneStepBtn.onClick.RemoveAllListeners();
		runOneStepBtn.onClick.AddListener (RunOneStep);
		runOneStepBtn.interactable = false; // initialise to false because player cannot run until at least a breakpoint is set

		//=======================================================================================
		previousOutputs.Clear ();	
		AddActiveIOToPreviousOutputs ();

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
		GameObject pathObject = currentPathObjects [potionStepCount];
		ExecutePotionScript potion = pathObject.GetComponent<ExecutePotionScript> ();

		int matchingIndex = -1;
		//if (potionStepCount == 0) {
			// output of first potion depends on the pathIndex
		//	matchingIndex = currentPathIndex;
		
		//} else {
			// non-first potions only receive one input when executed which can be determined by matching previous output
			Debug.Log("potion name: " + potion.gameObject.name);
			List<PotionScript.ListWrapper> actualInputs = potion.actualInputs;

			matchingIndex = MatchInputOutput (actualInputs);
	//	}

		// increase step count and check if further steps are allowed
		potionStepCount ++;
		PopulatePreviousOutputs (potion, matchingIndex);

		Debug.Log (" potion: " + potion.gameObject.name + " has breakpioint ? : " + potion.PotionHasBreakpoint ());
		Debug.Log ("matching index : " + matchingIndex);
		if (potion.PotionHasBreakpoint ()) {
			// hide original inputs and outputs 	
			HideOriginalInputOutput (potion, matchingIndex);

			// display actual inputs and outputs
			DisplayActualInputOutput (potion, matchingIndex);
			potion.ClearBreakpoint ();

			// add cost for displaying actual inputs and outputs
			LevelData.addCost(1);
			LevelData.addExecutedPotion (new PotionPathIndexPair(potion.gameObject.name, currentPathIndex, matchingIndex));

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
			if (!currentPathObjects.Contains (potion)) {
				potion.GetComponent<BoxCollider2D> ().enabled = false;
			} else {
				potion.GetComponent<BoxCollider2D> ().enabled = true;
			}
		}
	}

	private void ToggleActiveFlagOnOtherInputs(){
		foreach (GameObject otherInput in otherNeighbourInputs) {
			ExecutePathSelectScript epss = otherInput.GetComponent<ExecutePathSelectScript> ();
			epss.isActive = false;
		}	
	}

	private void DisplayActualInputOutput(ExecutePotionScript potion, int index){
		Debug.Log ("potion to display: " + potion.gameObject.name);
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
		//previousOutputs.Clear ();
		Debug.Log ("index is : " + index);
		PotionScript.ListWrapper actualOutputs = potion.actualOutputs [index];

		previousOutputs.Add (actualOutputs.GetSpriteList());
	}

	private void HideOriginalInputOutput(ExecutePotionScript potion, int index){

		List<GameObject> originalInputs = potion.inputs [index].list;
		foreach (GameObject originalInput in originalInputs) {
			originalInput.SetActive (false);
		}

		List<GameObject> originalOutputs = potion.outputs [index].list;
		foreach (GameObject originalOutput in originalOutputs) {
			originalOutput.SetActive (false);
		}
	}

	private int MatchInputOutput(List<PotionScript.ListWrapper> actualInputs){
		List<Sprite> matchedList = new List<Sprite> ();

		// debug
		foreach (List<Sprite> gb in previousOutputs){
			foreach (Sprite sprite in gb) {
				Debug.Log (" previous outputs: " + sprite.name);
			}
		}
		//
		foreach (PotionScript.ListWrapper actualInput in actualInputs) {
			foreach (Sprite sprite in actualInput.GetSpriteList()) {
				Debug.Log ("given input name : " + sprite.name);

				foreach (List<Sprite> potionOutputs in previousOutputs){
					if (potionOutputs.Contains(sprite) && !matchedList.Contains(sprite)){
						matchedList.Add (sprite);
					}
				}
			}
		}
		// debug
		foreach (Sprite s in matchedList){
			Debug.Log ("sprite name: " + s.name);
		}

		for (int i = 0; i < actualInputs.Count; i++) {
			List<Sprite> actualInput = actualInputs [i].GetSpriteList();
			if (CheckSpriteListEquivalence (actualInput, matchedList)) {
				return i;
			}
		}
		return -1;
	}

	private bool CheckSpriteListEquivalence(List<Sprite> listOne, List<Sprite> listTwo){
		// ! DO NOT delete the following commented code
		//if (listOne.Count != listTwo.Count) {
		//	return false;
		//}

		foreach (Sprite gb in listOne) {
			if (!listTwo.Contains(gb)) {
				return false;
			}
		}
		return true;
	}

	private bool StillHasBreakpoints(){
		foreach (GameObject gameObject in currentPathObjects) {
			
			ExecutePotionScript eps = gameObject.GetComponent<ExecutePotionScript> ();
			if (eps.PotionHasBreakpoint ()) {
				return true;
			}
		}
		return false;
	}

	private int GetCurrentPathIndex(){
		List<int> tempPathIndex = pathIndex;
		foreach (GameObject otherInput in otherInputs) {
			ExecutePathSelectScript epss = otherInput.GetComponent<ExecutePathSelectScript> ();
			if (epss.isActive) {
				return FindIntersection (epss.pathIndex, pathIndex);
			}
		}
		// if no other input is active - there should be one element in pathIndex
		return tempPathIndex[0];
	}

	private int FindIntersection(List<int> listOne, List<int> listTwo){
		// !!!!!! This method assumes there is at most one mutual element in two lists
		foreach (int i in listOne) {
			if (listTwo.Contains (i)) {
				return i;
			}
		}
		return -1; // It would be a mistake if two lists do not have a mutual element - exception
	}

	private void AddActiveIOToPreviousOutputs(){
		GameObject[] initialIO = GameObject.FindGameObjectsWithTag("IO");
		List<Sprite> sprites = new List<Sprite> ();

		foreach (GameObject gb in initialIO) {
			ExecutePathSelectScript epss = gb.GetComponent<ExecutePathSelectScript> ();

			if ( epss == null || epss.isActive ) {
				Sprite sprite = gb.GetComponent<SpriteRenderer> ().sprite;
				Debug.Log ("add sprite : " + sprite.name + " object name ; " + gb.name);
				sprites.Add (sprite);
			}
		}
		previousOutputs.Add (sprites);
	}
}

