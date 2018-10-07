using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpdateCursor_ramp_button : MonoBehaviour, IPointerDownHandler {

	public bool carrying;		// only used in object script to update frame with cursor controlling
	public bool ready_to_place;
	public bool can_place_to_false;



	public Texture2D ramp_texture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot;





	// Use this for initialization
	void Start () {
		
		
	}

	// Update is called once per frame
	void Update () {


	}

	public void OnPointerDown (PointerEventData eventData)  {
		// If lever mouse clicks on ramp, means to deselect
		if (carrying) {
			// deselect -> change to normal mouse
			carrying = false;
			ready_to_place = false;
			can_place_to_false = true;
		} 
		// If regular mouse clicks on ramp
		else {

			// change mouse to ramp
			carrying = true;
			ready_to_place = true;		// tell detect_click that it's ready for placement
			can_place_to_false = false;
		}


	}



	public void SetRamp() {
		hotSpot = new Vector2 (ramp_texture.width / 2, ramp_texture.height / 2);
		Cursor.SetCursor (ramp_texture, hotSpot, cursorMode);
	}
}
