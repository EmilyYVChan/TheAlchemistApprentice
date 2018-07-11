using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SystemViewControl : MonoBehaviour {

    [SerializeField]
    List<GameObject> systemViewPotionIOObjs = new List<GameObject>();

    public void updateSystemView()
    {
        bool[] inspectedPotions = JournalData.getListOfInspectedPotionsThusFar();

        for (int i = 0; i < systemViewPotionIOObjs.Count; i++)
        {
            GameObject potionObj = systemViewPotionIOObjs[i];
            int potionNumberOfObj = Int32.Parse(Regex.Match(potionObj.gameObject.name, @"\d+").Value);
            Debug.Log("potionNumOfObj = " + potionNumberOfObj);

            if (inspectedPotions[potionNumberOfObj - 1] == true)
            {
                potionObj.SetActive(true);
            } else
            {
                potionObj.SetActive(false);
            }
        }

        /*
        for (int i = 0; i < inspectedPotions.Length; i++)
        {
            //Debug.Log("inspectedPotion[" + i + "] = " + inspectedPotions[i]);
            if (inspectedPotions[i] == true)
            {
                systemViewPotionIOObjs[i].SetActive(true);
            } else
            {
                systemViewPotionIOObjs[i].SetActive(false);
            }
        }*/
    }

}
