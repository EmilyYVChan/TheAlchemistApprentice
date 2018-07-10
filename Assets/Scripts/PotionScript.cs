using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour {

    [System.Serializable]
    public class ListWrapper
    {
        public List<GameObject> list;
		public List<Sprite> GetSpriteList(){
			List<Sprite> spriteList = new List<Sprite> ();
			foreach (GameObject gb in list) {
				spriteList.Add (gb.GetComponent<SpriteRenderer> ().sprite);
			}
			return spriteList;
		}
    }

    public List<ListWrapper> inputs;
	public List<ListWrapper> outputs;
    public List<GameObject> slashes;

	public List<GameObject> formulae;

    private Text costTextUI;

    private int costOfInspectionPerFormula = 1;

	public GameObject dialogue;

    // Use this for initialization
	public virtual void Start () {

        costTextUI = GameObject.Find("Cost").GetComponent<Text>();
		//Debug.Log ("inspected " + LevelData.isPotionInspected (this.gameObject.name)+ " "+this.gameObject.name);

		if (!LevelData.isPotionInspected (this.gameObject.name)) {
			HideAndShowInputsOutputs (false);

			foreach (GameObject formula in formulae) {
				formula.SetActive (false);
			}
		} else {
			HideAndShowInputsOutputs (true);

			foreach (GameObject formula in formulae) {
				formula.SetActive (true);
			}
		}
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

	public void HideAndShowInputsOutputs(bool isVisible){
		foreach (ListWrapper listWrapper in inputs) {
			List<GameObject> inputRow = listWrapper.list;
			foreach (GameObject gameObject in inputRow) {
				if (!gameObject.tag.Equals ("IO")) {
					gameObject.SetActive (isVisible);
				}
			}                
		}

		foreach (ListWrapper listWrapper in outputs) {
			List<GameObject> outputRow = listWrapper.list;
			foreach (GameObject gameObject in outputRow) {
				if (!gameObject.tag.Equals ("IO")) {
					gameObject.SetActive (isVisible);
				}
			}                
		}
//
//		foreach (GameObject gameObject in slashes) {
//			if (!gameObject.tag.Equals ("IO")) {
//				gameObject.SetActive (isVisible);
//			}
//		}
	}

    public virtual void OnMouseDown()
	{
		int currentCost = LevelData.getCurrentMana ();
		if ((currentCost - (costOfInspectionPerFormula * inputs.Count) >= 0) || LevelData.isPotionInspected(this.gameObject.name)) {

			HideAndShowInputsOutputs (true);
			if (!LevelData.isPotionInspected(this.gameObject.name))
			{
				LevelData.addCost(costOfInspectionPerFormula * inputs.Count * -1);
				LevelData.addInspectedPotion (this.gameObject.name);
			}

			if (!JournalData.isPotionInspected(this.gameObject.name))
			{
				JournalData.addInspectedPotion(this.gameObject.name);
			}

			// display formula in dialogue
			UpdateDialogue ();

			// disable buttons behind the dialogue
			DisableButtons();
		}
    }
		
	private void UpdateDialogue()
	{
		dialogue.SetActive (true);

		// clear existing children
		foreach (Transform child in dialogue.transform)
		{
			if (child.gameObject.tag.Equals("Formula"))
			{
				child.gameObject.SetActive (false);
			}
		}

		foreach (GameObject formula in formulae)
		{
			formula.SetActive (true);
			formula.transform.SetParent(dialogue.transform);
		}
	}

	private void DisableButtons()
	{
		Button exitBtn = GameObject.Find ("ExitBtn").GetComponent<Button>();
		exitBtn.interactable = false;
		Button diagnoseBtn = GameObject.Find ("DiagnoseBtn").GetComponent<Button>();
		diagnoseBtn.interactable = false;
		Button nextBtn = GameObject.Find ("NextBtn").GetComponent<Button>();
		nextBtn.interactable = false;
	}
}
