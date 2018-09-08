using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerInspect : MonoBehaviour {

    public List<GameObject> tutorialGuides;
    public static int currentGuide = 0;

    public GameObject Tut_InspectComponent1;
    public GameObject Tut_Cost1;
    public GameObject Tut_InspectComponent2;
    public GameObject Tut_Cost2;
    public GameObject Tut_Execute;

    private BoxCollider2D colliderCost1;
    private BoxCollider2D colliderCost2;

    private BoxCollider2D[] allBoxCollidersInScene;

    public GameObject Dialogue;
    
    private static bool shouldShowInspectComponent = true;
    private static bool shouldShowComponentBhvrDialogue = false;
    private static bool shouldShowComponentBhvrGraph = false;
    private static bool shouldShowCost = false;
    private static bool shouldShowIteration = false;
    private static bool shouldShowDiagnose = false;
    private static bool shouldShowExecute = false;

    private static bool hasAlreadyInspectedOneComponent = false;
    private static bool hasBeenToExecuteStage = false;
    private static bool hasShownExecute = false;

    private static bool isSecondIteration = false;


    // Use this for initialization
    void Start ()
    {
        colliderCost1 = Tut_Cost1.transform.GetChild(3).GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        colliderCost2 = Tut_Cost2.transform.GetChild(3).GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;

        disableAllTutorialComponents();

        if (currentGuide == -1) { return; }

        tutorialGuides[currentGuide].SetActive(true);

        allBoxCollidersInScene = FindObjectsOfType<BoxCollider2D>();
    }

    void Update()
    {
        if (currentGuide == -1) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                string hitColliderGameObjectName = hit.collider.gameObject.name;
                if ((hitColliderGameObjectName == "RedPotion" && currentGuide == 0) || (hitColliderGameObjectName == "BluePotion" && currentGuide == 2))
                {
                    moveToNextTutorialGuide();
                }
            }         
        }
    }

    public static void setIsSecondIteration()
    {
        isSecondIteration = true;
        shouldShowIteration = true;
        hasBeenToExecuteStage = true;
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

        colliderCost1.enabled = true;
        colliderCost2.enabled = true;
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
        if (currentGuide == 1 || currentGuide == 3)
        {
            disableEveryOtherBoxCollider();
        } else
        {
            enableAllBoxCollider();
        }

        if (currentGuide == -1) { return; }
        tutorialGuides[currentGuide].SetActive(true);
    }
}
