using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCursor : MonoBehaviour {

	public Texture2D mouse;
	public Texture2D ill_lever;
	public Texture2D ill_ramp;
	public Texture2D ill_wedge;
	public Texture2D rotate_sign;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public float starty;

	// Use this for initialization
	void Start () {
		starty = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x, starty, this.transform.position.z);
		hotSpot = Vector2.zero;
		Cursor.SetCursor (mouse, hotSpot, cursorMode);
	}


	public void setMouse() {
		hotSpot = Vector2.zero;
		Cursor.SetCursor (mouse, hotSpot, cursorMode);
	}

	public void setIllLever() {
		hotSpot = new Vector2 (ill_lever.width / 2, ill_lever.height / 2);
		Cursor.SetCursor (ill_lever, hotSpot, cursorMode);
	}

	public void setIllRamp() {
		hotSpot = new Vector2 (ill_ramp.width / 2, ill_ramp.height / 2);
		Cursor.SetCursor (ill_ramp, hotSpot, cursorMode);
	}

	public void setIllWedge() {
		hotSpot = new Vector2 (ill_wedge.width / 2, ill_wedge.height / 2);
		Cursor.SetCursor (ill_wedge, hotSpot, cursorMode);
	}

	public void setRotateSign() {
		hotSpot = new Vector2 (rotate_sign.width / 2, rotate_sign.height / 2);
		Cursor.SetCursor (rotate_sign, hotSpot, cursorMode);
	}
}


