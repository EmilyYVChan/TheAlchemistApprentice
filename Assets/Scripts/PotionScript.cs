using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PotionScript : MonoBehaviour {

    [System.Serializable]
    public class ListWrapper
    {
        public List<GameObject> list;
    }

    public List<ListWrapper> inputs;
    public List<GameObject> outputs;
    public List<GameObject> slashes;

	public Sprite addSprite;
	public Sprite equalSprite;

    private Text costTextUI;

    private SpriteRenderer spriteR;

    private bool isAlreadyInspected;

    private int costOfInspectionPerFormula = 1;

	public GameObject dialogue;

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

		// display formula in dialogue
		UpdateDialogue ();
    }
		
	private void UpdateDialogue()
	{
		dialogue.SetActive (true);

		/**Transform originalAnchorPosition = GameObject.Find ("Anchor").transform;
		Transform anchorPosition = originalAnchorPosition;

		foreach (Transform child in dialogue.transform)
		{
			GameObject.Destroy (child.gameObject);
		}
		// reset text
		for (int i = 0; i < inputs.Count; i ++)
		{
			List<GameObject> formulaRow = inputs [i].list; 

			// format inputs
			for (int j = 0; j < formulaRow.Count; j ++)
			{
				GameObject input = formulaRow [j];

				GameObject formulaInput = new GameObject ("Input" + j);
				formulaInput.AddComponent<SpriteRenderer> ().sprite = input.GetComponent<SpriteRenderer> ().sprite;
				formulaInput.transform.SetParent (dialogue.transform);
				formulaInput.transform.position = new Vector2 (originalAnchorPosition.position.x + (0.5f * (j+1)), anchorPosition.position.y);
				anchorPosition = formulaInput.transform;

				if (j != formulaRow.Count - 1) 
				{
					GameObject add = new GameObject ("Add");
					add.AddComponent<SpriteRenderer> ().sprite = addSprite;
					add.transform.SetParent (dialogue.transform);
					add.transform.position = new Vector2 (anchorPosition.position.x + 0.5f, anchorPosition.position.y);
					anchorPosition = add.transform;
				}
			}

			// format output
			GameObject equal = new GameObject ("Equal");
			equal.AddComponent<SpriteRenderer> ().sprite = equalSprite;
			equal.transform.SetParent (dialogue.transform);
			equal.transform.position = new Vector2 (anchorPosition.position.x + 0.5f, anchorPosition.position.y);

			anchorPosition.transform.position = new Vector2 (originalAnchorPosition.position.x, originalAnchorPosition.position.y + 0.7f);

		}**/
	}
}
