using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecutePotionScript : PotionScript
{
	public List<ListWrapper> actualInputs;
	public List<ListWrapper> actualOutputs;
	public List<GameObject> pipes;

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
	}
}

