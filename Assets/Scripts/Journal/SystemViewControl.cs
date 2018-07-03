using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemViewControl : MonoBehaviour {

    [SerializeField]
    List<GameObject> systemViewPotionIOObjs = new List<GameObject>();

    public void updateSystemView()
    {
        bool[] inspectedPotions = JournalData.getListOfInspectedPotionsThusFar();
        for (int i = 0; i < inspectedPotions.Length; i++)
        {
            Debug.Log("inspectedPotion[" + i + "] = " + inspectedPotions[i]);
            if (inspectedPotions[i] == true)
            {
                systemViewPotionIOObjs[i].SetActive(true);
            } else
            {
                systemViewPotionIOObjs[i].SetActive(false);
            }
        }
    }

}
