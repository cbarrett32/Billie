using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Gamekit2D;

public class UpdateCursor_crossbow_button : MonoBehaviour, IPointerDownHandler {
	Detect_click dc;
	UpdateCursor_eraser_button eraser;

	public bool carrying;		
	public bool ready_to_place;
	public bool can_place_to_false;

	// for setting cursors:
	public Texture2D crosshair_texture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	private Animator animator;
	public GameObject billie;
	public Sprite billie_shoot;
	public Sprite billie_idle;

	GameObject panel_arrow;



	bool first = true;


	// Use this for initialization
	void Start () {
		animator = GameObject.Find("Billie").GetComponent<Animator>();
		dc = GameObject.Find ("GameObject_cursor").GetComponent<Detect_click> ();
		eraser = GameObject.Find ("Eraser").GetComponent<UpdateCursor_eraser_button> ();

		// arrow helpbox for shooting:
		panel_arrow = GameObject.Find ("help_arrow");
		panel_arrow.SetActive (false);

	}

	// Update is called once per frame
	void Update () {

	}

	// when the mouse clicks on lever inventory
	public void OnPointerDown (PointerEventData eventData)  {
		// carrying: means that the mouse is now the lever


		if (carrying) {		// reclick the lever button -> want to deselect lever and change mouse back to normal
			animator.SetBool("carry_crossbow", false);
			billie.GetComponent<SpriteRenderer> ().sprite = billie_idle;
			carrying = false;
			ready_to_place = false;
			can_place_to_false = true;	// passed on to detect_click to not place the lever when deselecting
		} 
		else {	// to select lever from the inventory
			if (first) {
				StartCoroutine(Blink(panel_arrow));
				first = false;
			}


			eraser.ready_to_place = false;
			dc.can_erase = false;

			animator.SetBool("carry_crossbow", true);
			billie.GetComponent<SpriteRenderer> ().sprite = billie_shoot;
			carrying = true;
			ready_to_place = true;		// passed on to detect_click(general control) that lever is ready to be placed
			can_place_to_false = false;
		}


	}

	// change the cursor to lever
	public void SetCrosshair() {
		hotSpot = new Vector2 (crosshair_texture.width / 2, crosshair_texture.height / 2);
		Cursor.SetCursor (crosshair_texture, hotSpot, cursorMode);
	}

	IEnumerator Blink(GameObject panel_arrow){
		//blink it forever. 
		while(true){
			if (dc.first_shoot == false) {
				panel_arrow.SetActive (false);
				break;
			}
			if (panel_arrow != null) {
				panel_arrow.SetActive (true);
				//display blank text for 0.5 seconds
				yield return new WaitForSeconds (.5f);
			}

			if (panel_arrow != null) {
				panel_arrow.SetActive (false);
				yield return new WaitForSeconds (.5f);
			}
		}
	}
}

