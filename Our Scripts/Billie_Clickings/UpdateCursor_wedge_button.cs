using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpdateCursor_wedge_button : MonoBehaviour, IPointerDownHandler {

	public bool carrying;		
	public bool ready_to_place;
	public bool can_place_to_false;

	// for setting cursors:
	public Texture2D wedge_texture;
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
		// carrying: means that the mouse is now the wedge


		if (carrying) {					// reclick the wedge button -> want to deselect wedge and change mouse back to normal
			carrying = false;
			ready_to_place = false;
			can_place_to_false = true;	// passed on to detect_click to not place the wedge when deselecting
		} 
		else {							// to select wedge from the inventory
			carrying = true;
			ready_to_place = true;		// passed on to detect_click(general control) that wedge is ready to be placed
			can_place_to_false = false;
		}


	}

	// change the cursor to lever
	public void SetWedge() {
		hotSpot = new Vector2 (wedge_texture.width / 2, wedge_texture.height / 2);
		Cursor.SetCursor (wedge_texture, hotSpot, cursorMode);
	}
}
