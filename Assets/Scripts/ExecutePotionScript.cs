using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecutePotionScript : PotionScript
{
	public List<ListWrapper> actualInputs;
	public List<GameObject> actualOutputs;
	public List<GameObject> pipes;

	// Use this for initialization
	public override void Start ()
	{
		// does what PotionScript does
		base.Start ();

		// hides actual inputs and outputs
		foreach (ListWrapper listWrapper in actualInputs) {
			List<GameObject> inputRow = listWrapper.list;
			foreach (GameObject gameObject in inputRow) {
				if (!gameObject.tag.Equals ("IO")) {
					gameObject.SetActive (false);
				}
			}                
		}

		foreach (GameObject gameObject in actualOutputs){
			gameObject.SetActive (false);
		}
	}

	public override void OnMouseDown()
	{
	}
}

