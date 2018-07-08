using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsViewControl : MonoBehaviour {

    public GameObject pathsViewsGameObj;

    public void updatePathsView()
    {
        bool[] inspectedPotions = JournalData.getListOfInspectedPotionsThusFar();
        Transform[] pathsViewChildren = pathsViewsGameObj.GetComponentsInChildren<Transform>(true);

        //disable all potions IO
        foreach (Transform child in pathsViewChildren)
        {
            if (child.gameObject.name.Contains("PathsViewJournalp"))
            {
                Debug.Log("deactivating " + child.gameObject.name);
                child.gameObject.SetActive(false);
            }
        }

        //enable only the potions IO that have been inspected
        for (int i = 0; i < inspectedPotions.Length; i++)
        {
            Debug.Log("inspectedPotion[" + i + "] = " + inspectedPotions[i]);
            if (inspectedPotions[i] == true)
            {
                foreach (Transform child in pathsViewChildren)
                {
                    Debug.Log("child name = " + child.gameObject.name);
                    if (child.gameObject.name.Equals("PathsViewJournalp" + (i + 1).ToString() + "IO"))
                    {
                        Debug.Log("activating " + child.gameObject.name);
                        child.gameObject.SetActive(true);
                    } 
                }
            }
        }
    }
}
