using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour {

    [SerializeField]
    private GameObject buttonTemplate;
    //[SerializeField]
    //private int[] intArray;
    private int maxCount = 10;

    private List<GameObject> buttons;

    // Use this for initialization
    void Start()
    {
        buttons = new List<GameObject>();

        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }

            buttons.Clear();
        }

        for (int i = 1; i <= maxCount; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<ButtonListButton>().SetText("Button #" + i);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }

    }

    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }
}
