using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagnosePotionScript : ExecutePotionScript {
	public bool isFault;
	public GameObject canvasResult;

	// Use this for initialization
	public override void Start (){
		base.Start ();
	}

	public override void OnMouseDown()
	{
		if (isFault) {
			canvasResult.SetActive(true);
		} else {
			Debug.Log ("not fault");
		}
	}
}
