using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour {
	private GameObject dialogue;

	// Use this for initialization
	void Start () {
		dialogue = GameObject.Find("Dialogue");
		dialogue.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HideDialogue()
	{
		dialogue.SetActive (false);
		EnableButtons ();

		// set ManaWarning to be the default display message in Operate Stages
		// ONLY operate stage has ManaWarning and PathWarning messages. Other stages only have Warning message
		foreach (Transform child in dialogue.transform)
		{
			if (child.gameObject.name.Equals ("ManaWarning")) {
				child.gameObject.SetActive (true);
			} 
			else if (child.gameObject.name.Equals ("PathWarning")) {
				child.gameObject.SetActive (false);				
			}
		}
	}

	private void EnableButtons()
	{
		Button exitBtn = GameObject.Find ("ExitBtn").GetComponent<Button>();
		exitBtn.interactable = true;
//		Button diagnoseBtn = GameObject.Find ("DiagnoseBtn").GetComponent<Button>();
//		diagnoseBtn.interactable = true;
//		Button nextBtn = GameObject.Find ("NextBtn").GetComponent<Button>();
//		nextBtn.interactable = true;
	}
}
