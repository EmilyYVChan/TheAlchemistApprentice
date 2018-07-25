using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerDiagnose : MonoBehaviour {

    public GameObject Tut_ChoosePotion;

    private static bool shouldShowChoosePotion = true;

    // Use this for initialization
    void Start () {
        if (shouldShowChoosePotion)
        {
            Tut_ChoosePotion.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Tut_ChoosePotion.SetActive(false);
            shouldShowChoosePotion = false;
        }
    }
}
