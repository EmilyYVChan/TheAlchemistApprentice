using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerExecute : MonoBehaviour {

    public GameObject Tut_DimmedExpected;
    public GameObject Tut_ChooseInput;
    public GameObject Tut_SetBreakpoint;
    public GameObject Tut_SetBreakpoint2;
    public GameObject Tut_Run1Step;
    public GameObject Tut_ViewActual;
    public GameObject Tut_Run1Step2;
    public GameObject Tut_ReplacedExpected;
    public GameObject Tut_NoBreakpoint;

    private static bool shouldShowDimmedExpected = true;
    private static bool shouldShowChooseInput = false;
    private static bool shouldShowSetBreakpoint = false;
    private static bool shouldShowSetBreakpoint2 = false;
    private static bool shouldShowRun1Step = false;
    private static bool shouldShowViewActual = false;
    private static bool shouldShowRun1Step2 = false;
    private static bool shouldShowReplacedExpected = false;
    private static bool shouldShowNoBreakpoint = false;

    //private static bool skipShowRun1Step2 = false;

    // Use this for initialization
    void Start()
    {
        disableAllTutorialComponents();

        if (shouldShowDimmedExpected)
        {
            Tut_DimmedExpected.SetActive(true);
            shouldShowDimmedExpected = false;
            shouldShowChooseInput = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                string hitColliderGameObjectName = hit.collider.gameObject.name;
                Debug.Log(hitColliderGameObjectName);
                if (hitColliderGameObjectName == "air"
                    && Tut_ChooseInput.activeSelf == true)
                {
                    disableAllTutorialComponents();
                    shouldShowChooseInput = false;
                    shouldShowSetBreakpoint = true;
                } else if (hitColliderGameObjectName == "PinkPotion"
                    && Tut_SetBreakpoint.activeSelf == true)
                {
                    disableAllTutorialComponents();
                    shouldShowSetBreakpoint = false;
                    shouldShowSetBreakpoint2 = true;
                } else if (hitColliderGameObjectName == "RedPotion"
                    && Tut_SetBreakpoint2.activeSelf == true)
                {
                    disableAllTutorialComponents();
                    shouldShowSetBreakpoint2 = false;
                    shouldShowRun1Step = true;
                } else if (hitColliderGameObjectName == "RunStepBtn"
                    && Tut_Run1Step.activeSelf == true)
                {
                    disableAllTutorialComponents();
                    shouldShowRun1Step = false;
                    shouldShowViewActual = true;
                } else if (hitColliderGameObjectName == "RunStepBtn"
                    && Tut_Run1Step2.activeSelf == true)
                {
                    disableAllTutorialComponents();
                    shouldShowRun1Step2 = false;
                    shouldShowReplacedExpected = true;
                } else if (hitColliderGameObjectName == "RunStepBtn"
                    && Tut_ViewActual.activeSelf == true)
                {
                    disableAllTutorialComponents();
                    shouldShowRun1Step2 = false;
                    shouldShowReplacedExpected = true;
                }
            }
            else
            {
                disableAllTutorialComponents();

                if (shouldShowDimmedExpected && !shouldShowChooseInput)
                {
                    shouldShowDimmedExpected = false;
                    shouldShowChooseInput = true;
                }
            }
        }
        updateComponentActiveness();
    }

    //------------Helper methods
    private void disableAllTutorialComponents()
    {
        Tut_DimmedExpected.SetActive(false);
        Tut_ChooseInput.SetActive(false);
        Tut_SetBreakpoint.SetActive(false);
        Tut_SetBreakpoint2.SetActive(false);
        Tut_Run1Step.SetActive(false);
        Tut_ViewActual.SetActive(false);
        Tut_Run1Step2.SetActive(false);
        Tut_ReplacedExpected.SetActive(false);
        Tut_NoBreakpoint.SetActive(false);
    }

    private void updateComponentActiveness()
    {
        if (shouldShowChooseInput && Tut_DimmedExpected.activeSelf == false)
        {
            Tut_ChooseInput.SetActive(true);
        }

        if (shouldShowSetBreakpoint && Tut_ChooseInput.activeSelf == false)
        {
            Tut_SetBreakpoint.SetActive(true);
        }

        if (shouldShowSetBreakpoint2 && Tut_SetBreakpoint.activeSelf == false)
        {
            Tut_SetBreakpoint2.SetActive(true);
        }

        if (shouldShowRun1Step && Tut_SetBreakpoint2.activeSelf == false)
        {
            Tut_Run1Step.SetActive(true);
        }

        if (shouldShowViewActual && Tut_Run1Step.activeSelf == false)
        {
            Tut_ViewActual.SetActive(true);
            shouldShowViewActual = false;
            shouldShowRun1Step2 = true;
        }

        if (shouldShowRun1Step2 && Tut_ViewActual.activeSelf == false)
        {
            Tut_Run1Step2.SetActive(true);
        }

        if (shouldShowReplacedExpected && Tut_Run1Step2.activeSelf == false)
        {
            Tut_ReplacedExpected.SetActive(true);
            shouldShowReplacedExpected = false;
            shouldShowNoBreakpoint = true;
        }

        if (shouldShowNoBreakpoint && Tut_ReplacedExpected.activeSelf == false)
        {
            Tut_NoBreakpoint.SetActive(true);
            shouldShowNoBreakpoint = false;
        }
    }
}
