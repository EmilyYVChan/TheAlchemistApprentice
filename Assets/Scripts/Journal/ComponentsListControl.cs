using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsListControl : MonoBehaviour {
    
    [SerializeField]
    private List<GameObject> componentTemplates;

    [SerializeField]
    private GameObject p1Template;
    [SerializeField]
    private GameObject p2Template;

    [SerializeField]
    private GameObject buttonTemplate;

    //[SerializeField]
    //private int[] intArray;
    private int count = 1;

    private List<GameObject> buttons;
    private List<GameObject> componentJournalItems;

    void Start()
    {
        componentJournalItems = new List<GameObject>();
    }

    // Use this for initialization
    public void generateList()
    {
        if (componentJournalItems.Count > 0)
        {
            foreach (GameObject item in componentJournalItems)
            {
                Destroy(item.gameObject);
            }
            componentJournalItems.Clear();
        }

        /*for (int i = 0; i < count; i++)
        {
            GameObject journalItem = Instantiate(p1Template) as GameObject;
            journalItem.SetActive(true);

            journalItem.transform.SetParent(p1Template.transform.parent, false);
        }
        count++;*/

        bool[] recordedPotions = JournalData.getListOfInspectedPotionsThusFar();
        for (int i = 0; i < recordedPotions.Length; i++)
        {
            Debug.Log("recordedPotion[" + i + "] = " + recordedPotions[i]);
            if (recordedPotions[i] == true)
            {
                GameObject journalItem = Instantiate(componentTemplates[i]) as GameObject;
                journalItem.SetActive(true);

                journalItem.transform.SetParent(componentTemplates[i].transform.parent, false);

                componentJournalItems.Add(journalItem);
            }
        }

    }

    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }
}
