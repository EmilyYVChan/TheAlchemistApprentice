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

}
