using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Rotate_sign : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	HandleCursor cursor;
	bool enter;
	Detect_click dc;

	// Use this for initialization
	void Start () {
		// cursor:
		cursor = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<HandleCursor> ();
		dc = GameObject.Find ("GameObject_cursor").GetComponent<Detect_click> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (enter && !dc.can_erase && !dc.hitramp && !dc.hitlever) {
			cursor.setRotateSign ();
		} 
	}

	// show rotate sign
	public void OnPointerEnter (PointerEventData eventData) {
		enter = true;
	}

	public void OnPointerExit (PointerEventData eventData) {
		enter = false;
	}
}
