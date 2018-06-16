using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecutePathSelectScript : MonoBehaviour
{
	public List<GameObject> otherInputs;
	public bool isDefault;
	public List<GameObject> pathObjects;
	// Use this for initialization
	void Start ()
	{
		if (!isDefault) {
			// render input semi-transparent if this input is not default
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .4f);
		} 
		else {
			// highlight the associated control flow path if this input is default
			changePipeColour(pathObjects,new Color (1f, 1f, 0f, 1f)); // yellow
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnMouseDown()
	{
		// change input opacity
		foreach (GameObject otherInput in otherInputs) {
			otherInput.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .4f);
		}

		this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);

		// un-highlight other control flow path
		foreach (GameObject otherInput in otherInputs){
			ExecutePathSelectScript epss = otherInput.GetComponent<ExecutePathSelectScript> ();
			List<GameObject> otherPathObjects = epss.pathObjects;
			changePipeColour(otherPathObjects,new Color (1f, 1f, 1f, 1f)); // white
		}

		// highlight control flow path
		changePipeColour(pathObjects,new Color (1f, 1f, 0f, 1f)); // yellow
	}

	private void changePipeColour(List<GameObject> pathObjects, Color colour){
		foreach (GameObject pathObject in pathObjects){
			ExecutePotionScript potion = pathObject.GetComponent<ExecutePotionScript> ();

			List<GameObject> pipes = potion.pipes;
			foreach (GameObject pipe in pipes) {
				pipe.GetComponent<SpriteRenderer> ().color = colour;
			}
		}
	}
}

