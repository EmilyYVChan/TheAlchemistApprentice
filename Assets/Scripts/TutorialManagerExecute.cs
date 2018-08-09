using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerExecute : MonoBehaviour {

    public List<GameObject> tutorialGuides;
    public static int currentGuide = 0;

    public List<GameObject> tutorialGuidesWithOKButtons;
    private BoxCollider2D[] allBoxCollidersInScene;

    public GameObject handPointingToPot2;
    public GameObject handPointingToPot3;

    public GameObject airInput;
    public GameObject waterInput;

    // Use this for initialization
    void Start()
    {
        allBoxCollidersInScene = FindObjectsOfType<BoxCollider2D>();
        disableAllTutorialComponents();

        if (currentGuide == -1) { return; }

        updateComponentActiveness();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentGuide == -1) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                string hitColliderGameObjectName = hit.collider.gameObject.name;
                Debug.Log(hitColliderGameObjectName);
                if ((hitColliderGameObjectName == "waterInput1" && currentGuide == 1) ||
                    (hitColliderGameObjectName == "BluePotion" && currentGuide == 2) || 
                    (hitColliderGameObjectName == "PinkPotion" && currentGuide == 4))
                {
                    moveToNextTutorialGuide();
                }
            }
        }
    }

    //------------Helper methods
    public void moveToNextTutorialGuide()
    {
        if (currentGuide == -1) { return; };
        disableAllTutorialComponents();
        currentGuide++;
        if (currentGuide == tutorialGuides.Capacity)
        {
            currentGuide = -1;
        }
        updateComponentActiveness();
    }

    private void disableAllTutorialComponents()
    {
        foreach (GameObject go in tutorialGuides)
        {
            go.SetActive(false);
        }
    }

    private void disableEveryOtherBoxCollider()
    {
        foreach (BoxCollider2D b in allBoxCollidersInScene)
        {
            b.enabled = false;
        }

        foreach (GameObject go in tutorialGuidesWithOKButtons)
        {
            go.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void enableAllBoxCollider()
    {
        foreach (BoxCollider2D b in allBoxCollidersInScene)
        {
            b.enabled = true;
        }
    }

    private void updateComponentActiveness()
    {
        if (currentGuide == 0 || currentGuide == 3 || currentGuide == 6 || currentGuide == 7 || 
            currentGuide == 9 || currentGuide == 10 || currentGuide == 11 || currentGuide == 12 || currentGuide == 13)
        {
            if (currentGuide == 3)
            {
                if (airInput.GetComponent<ExecutePathSelectScript>().isActive == true) //potion 2 is on highlighted path
                {
                    handPointingToPot2.SetActive(false);
                    handPointingToPot3.SetActive(true);
                } else //potion 3 is on highlighted path
                {
                    handPointingToPot2.SetActive(true);
                    handPointingToPot3.SetActive(false);
                }
            }
            disableEveryOtherBoxCollider();
        }
        else
        {
            enableAllBoxCollider();
        }

        if (currentGuide == -1) { return; }
        tutorialGuides[currentGuide].SetActive(true);
    }

    public void runOneStepFollowingTutorial()
    {
        if (currentGuide == 5 || currentGuide == 8)
        {
            moveToNextTutorialGuide();
        }
    }
}
