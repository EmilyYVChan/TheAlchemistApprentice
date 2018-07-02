using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour {

    [SerializeField]
    private Text myText;
    [SerializeField]
    private ButtonListControl buttonControl;

    private string myTextString;

    public void SetText(string textString)
    {
        myText.text = textString;
        myTextString = textString;
    }

    public void OnClick()
    {
        buttonControl.ButtonClicked(myTextString);
    }
    
}
