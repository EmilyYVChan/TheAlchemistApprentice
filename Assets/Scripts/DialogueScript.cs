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
	}

	private void EnableButtons()
	{
		Button exitBtn = GameObject.Find ("ExitBtn").GetComponent<Button>();
		exitBtn.interactable = true;
		Button diagnoseBtn = GameObject.Find ("DiagnoseBtn").GetComponent<Button>();
		diagnoseBtn.interactable = true;
		Button nextBtn = GameObject.Find ("NextBtn").GetComponent<Button>();
		nextBtn.interactable = true;
	}
}
