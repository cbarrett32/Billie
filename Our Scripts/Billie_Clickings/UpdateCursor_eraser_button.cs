using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpdateCursor_eraser_button : MonoBehaviour, IPointerDownHandler {
	Detect_click dc;
	UpdateCursor_crossbow_button crossbow;



	public bool carrying;		
	public bool ready_to_place;
	public bool can_place_to_false;

	// for setting cursors:
	public Texture2D eraser_texture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	GameObject help_eraser_flash;

	bool first = true;


	// Use this for initialization
	void Start () {
		dc = GameObject.Find ("GameObject_cursor").GetComponent<Detect_click> ();
		crossbow = GameObject.Find ("Crossbow").GetComponent<UpdateCursor_crossbow_button> ();



		help_eraser_flash = GameObject.Find ("help_eraser_flash");
		help_eraser_flash.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	// when the mouse clicks on lever inventory
	public void OnPointerDown (PointerEventData eventData)  {
		// carrying: means that the mouse is now the eraser


		if (carrying) {					// reclick the eraser button -> want to deselect eraser and change mouse back to normal
			carrying = false;
			ready_to_place = false;
			can_place_to_false = true;	// passed on to detect_click to not place the eraser when deselecting
		} 
		else {	
			if (first) {
				StartCoroutine(Blink(help_eraser_flash));
				help_eraser_flash.SetActive (true);
				first = false;
			}
			dc.can_place_crossbow = false;
			crossbow.ready_to_place = false;
			// to select eraser from the inventory
			carrying = true;
			ready_to_place = true;		// passed on to detect_click(general control) that eraser is ready to be placed
			can_place_to_false = false;
		}


	}



	// change the cursor to lever
	public void SetEraser() {
		hotSpot = new Vector2 (eraser_texture.width / 2, eraser_texture.height / 2);
		Cursor.SetCursor (eraser_texture, hotSpot, cursorMode);
	}

	IEnumerator Blink(GameObject obj){
		//blink it forever. 
		while (true) {
			if (dc.first_erase == false) {
				obj.SetActive (false);
				break;
			}
			if (obj != null) {
				obj.SetActive (true);
				//display blank text for 0.5 seconds
				yield return new WaitForSeconds (.5f);
			}

			if (obj != null) {
				obj.SetActive (false);
				yield return new WaitForSeconds (.5f);
			}
		}
	}
}
