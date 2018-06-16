using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecutePotionScript : PotionScript
{
	public bool isFaulty;
	public List<GameObject> actualOutputs;
	public List<GameObject> pipes;

	// Use this for initialization
	public override void Start ()
	{
		// does what PotionScript does
		base.Start ();
	}

	public override void OnMouseDown()
	{
		if (isFaulty) {
			// display actual output	
		}else{
			// display original output (actual = original)
		}
	}
}

