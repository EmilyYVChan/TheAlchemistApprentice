using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagnosePotionScript : ExecutePotionScript {
	public bool isFault;

	// Use this for initialization
	public override void Start (){
		base.Start ();
	}
	
	public override void OnMouseDown()
	{
		if (isFault) {
			Debug.Log ("is fault");
		} else {
			Debug.Log ("not fault");
		}
	}
}
