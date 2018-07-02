using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour {

    GameObject canvasJournalObject;

	// Use this for initialization
	void Start () {
        canvasJournalObject = GameObject.Find("CanvasJournal");
        canvasJournalObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openJournal()
    {
        canvasJournalObject.SetActive(true);
    }

    public void closeJournal()
    {
        canvasJournalObject.SetActive(false);
    }

    public void openComponentsTab()
    {
        //close system tab
        //open components tab
    }

    public void openSystemsTab()
    {
        //close components tab
        //open systems tab
    }

    public void showSystemView(int level)
    {
        //disable all system views
        GameObject systemViewsGameObject = GameObject.Find("SystemViews");
        foreach (Transform child in systemViewsGameObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        //enable the desired system view
        if (level == 0)
        {
            systemViewsGameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (level == 1)
        {
            systemViewsGameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            systemViewsGameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }




    //------------------Helper methods
    private void disableAllSystemViews()
    {

    }
}
