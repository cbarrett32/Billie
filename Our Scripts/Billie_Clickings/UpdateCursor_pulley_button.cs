﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Gamekit2D;

public class UpdateCursor_pulley_button : MonoBehaviour, IPointerDownHandler {

	public bool carrying;		
	public bool ready_to_place;
	public bool can_place_to_false;

	// for setting cursors:
	public Texture2D pulley_texture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {


	}

	// when the mouse clicks on lever inventory
	public void OnPointerDown (PointerEventData eventData)  {
		// carrying: means that the mouse is now the lever


		if (carrying) {		// reclick the lever button -> want to deselect lever and change mouse back to normal
			carrying = false;
			ready_to_place = false;
			can_place_to_false = true;	// passed on to detect_click to not place the lever when deselecting
		} 
		else {	// to select lever from the inventory
			carrying = true;
			ready_to_place = true;		// passed on to detect_click(general control) that lever is ready to be placed
			can_place_to_false = false;
		}


	}

	// change the cursor to lever
	public void SetPulley() {
		hotSpot = new Vector2 (pulley_texture.width / 2, pulley_texture.height / 2);
		Cursor.SetCursor (pulley_texture, hotSpot, cursorMode);
	}
}
