using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour {

    [System.Serializable]
    public class ListWrapper
    {
        public List<GameObject> list;
    }

    public List<ListWrapper> inputs;
    public List<GameObject> outputs;
    public List<GameObject> slashes;

    private Text costTextUI;

    private SpriteRenderer spriteR;

    private bool isAlreadyInspected;

    private int costOfInspectionPerFormula = 1;

    // Use this for initialization
    void Start () {
        isAlreadyInspected = false;

        costTextUI = GameObject.Find("Cost").GetComponent<Text>();

        foreach (ListWrapper listWrapper in inputs) 
        {
            List<GameObject> inputRow = listWrapper.list;
            foreach (GameObject gameObject in inputRow)
            {
                gameObject.SetActive(false);
            }                
        }

        foreach (GameObject gameObject in outputs)
        {
            gameObject.SetActive(false);
        }

        foreach (GameObject gameObject in slashes)
        {
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        foreach (ListWrapper listWrapper in inputs)
        {
            List<GameObject> inputRow = listWrapper.list;
            foreach (GameObject gameObject in inputRow)
            {
                gameObject.SetActive(true);
            }
        }

        foreach (GameObject gameObject in outputs)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in slashes)
        {
            gameObject.SetActive(true);
        }

        if (!isAlreadyInspected)
        {
            Debug.Log("entered !isAlreadyInspected ");
            string currentCostString = Regex.Match(costTextUI.text, @"\d+").Value;
            int oldCost = System.Int32.Parse(currentCostString);
            Debug.Log("oldCost = " + oldCost);
            int newCost = oldCost + (costOfInspectionPerFormula * inputs.Count);
            Debug.Log("newCost = " + newCost);
            string newCostString = Regex.Replace(costTextUI.text, @"\d", newCost.ToString());
            costTextUI.text = newCostString;
            isAlreadyInspected = true;
        }
    }


}
