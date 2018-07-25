using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerInspect : MonoBehaviour {
    public GameObject Tut_GameGoal;
    public GameObject Tut_InspectComponent;
    public GameObject Tut_ComponentBhvrDialogue;
    public GameObject Tut_ComponentBhvrGraph;
    public GameObject Tut_Cost;
    public GameObject Tut_Iteration;
    public GameObject Tut_Diagnose;
    public GameObject Tut_Execute;

    public GameObject Dialogue;
    public GameObject CanvasInspect;
    public GameObject TutorialGraph;

    private static bool shouldShowGameGoal = true;
    private static bool shouldShowInspectComponent = false;
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
    void Start () {
        //assignGameObjectsFields();
        disableAllTutorialComponents();

        if (shouldShowGameGoal)
        {
            Tut_GameGoal.SetActive(true);
            shouldShowGameGoal = false;
            disableOtherClickableUIs();
        }
                
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("hit.collider.gameobject.name = " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "RedPotion")
                {
                    disableAllTutorialComponents();
                    shouldShowComponentBhvrDialogue = true;
                    updateComponentActiveness();

                    hasAlreadyInspectedOneComponent = true;

                }
                else if (!hasShownExecute &&
                  hasAlreadyInspectedOneComponent &&
                  (hit.collider.gameObject.name == "YellowPotion" || hit.collider.gameObject.name == "PinkPotion" || hit.collider.gameObject.name == "BluePotion"))
                {
                    shouldShowExecute = true;
                }
            } else
            {
                enableOtherClickableUIs();
                disableAllTutorialComponents();
            }            
        }
        updateComponentActiveness();
    }

    public static void setIsSecondIteration()
    {
        isSecondIteration = true;
        shouldShowIteration = true;
        hasBeenToExecuteStage = true;
    }


    //------------Helper methods
    private void enableOtherClickableUIs()
    {
        CanvasInspect.SetActive(true);
        TutorialGraph.SetActive(true);
    }

    private void disableOtherClickableUIs()
    {
        CanvasInspect.SetActive(false);
        TutorialGraph.SetActive(false);
    }

    private void assignGameObjectsFields()
    {
        Tut_InspectComponent = GameObject.Find("Tut_InspectComponent");
        Tut_ComponentBhvrDialogue = GameObject.Find("Tut_ComponentBhvrDialogue");
        Tut_ComponentBhvrGraph = GameObject.Find("Tut_ComponentBhvrGraph");
        Tut_Cost = GameObject.Find("Tut_Cost");
        Tut_Iteration = GameObject.Find("Tut_Iteration");
        Tut_Diagnose = GameObject.Find("Tut_Diagnose");
        Tut_Execute = GameObject.Find("Tut_Execute");
    }

    private void disableAllTutorialComponents()
    {
        Tut_GameGoal.SetActive(false);
        Tut_InspectComponent.SetActive(false);
        Tut_ComponentBhvrDialogue.SetActive(false);
        Tut_ComponentBhvrGraph.SetActive(false);
        Tut_Cost.SetActive(false);
        Tut_Iteration.SetActive(false);
        Tut_Diagnose.SetActive(false);
        Tut_Execute.SetActive(false);
    }

    private void updateComponentActiveness()
    {
        if (shouldShowInspectComponent && Tut_GameGoal.activeSelf == false)
        {
            Tut_InspectComponent.SetActive(true);
            shouldShowInspectComponent = false;
        }

        if (shouldShowComponentBhvrDialogue && Tut_InspectComponent.activeSelf == false)
        {
            Tut_ComponentBhvrDialogue.SetActive(true);
            shouldShowComponentBhvrDialogue = false;
            shouldShowComponentBhvrGraph = true;
        }

        if (shouldShowComponentBhvrGraph && Tut_ComponentBhvrDialogue.activeSelf == false)
        {
            Tut_ComponentBhvrGraph.SetActive(true);
            shouldShowComponentBhvrGraph = false;
            shouldShowCost = true;
        }

        if (shouldShowCost && Tut_ComponentBhvrGraph.activeSelf == false)
        {
            Tut_Cost.SetActive(true);
            shouldShowCost = false;
        }

        if (isSecondIteration && shouldShowIteration)
        {
            Tut_Iteration.SetActive(true);
            shouldShowIteration = false;
            shouldShowDiagnose = true;
        }

        if (isSecondIteration && shouldShowDiagnose && Tut_Iteration.activeSelf == false)
        {
            Tut_Diagnose.SetActive(true);
            shouldShowDiagnose = false;
        }

        if (shouldShowExecute && Dialogue.activeSelf == false && !hasBeenToExecuteStage)
        {
            Tut_Execute.SetActive(true);
            shouldShowExecute = false;
            hasShownExecute = true;
        }
    }
}
