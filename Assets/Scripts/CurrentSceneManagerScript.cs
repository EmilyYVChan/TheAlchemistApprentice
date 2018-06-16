using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSceneManagerScript : MonoBehaviour {

    Text costTextUI; 

	// Use this for initialization
	void Start () {
        Text iterationCountTextUI = GameObject.Find("IterationCount").GetComponent<Text>();
        iterationCountTextUI.text = LevelData.getCurrentIteration().ToString();

        costTextUI = GameObject.Find("Cost").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        costTextUI.text = LevelData.getCurrentCost().ToString();
	}
}
