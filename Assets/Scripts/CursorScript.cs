﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    public Texture2D cursorClickableTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter()
	{
        Cursor.SetCursor(cursorClickableTexture, hotSpot, cursorMode);
        Debug.Log ("CursorScript.OnMouseEnter()");
	}

	void OnMouseOver()
	{
		
	}

	void OnMouseExit()
	{
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
		Debug.Log ("CursorScript.OnMouseExit()");
	}
}
