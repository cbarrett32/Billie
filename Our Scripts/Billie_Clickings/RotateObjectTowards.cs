using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotateObjectTowards : MonoBehaviour {
	Detect_click dc;


	public float offset = 180.0f;

	Vector3 startDragDir;
	Vector3 currentDragDir;
	Quaternion initialRotation;
	float angleFromStart;

	void Start() {
		dc = GameObject.Find ("GameObject_cursor").GetComponent<Detect_click> ();
	}

	void Update() {
		if (!dc.donot_rotate) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

				if (hit.collider != null) {
					GameObject parent_machine = hit.collider.gameObject;
					if (parent_machine.tag == "simplemachine") {
						if (parent_machine.transform.localEulerAngles.y == 0) {
							parent_machine.transform.localRotation = Quaternion.Euler (0, 180, 0);

							AudioSource ObjectRotate = parent_machine.GetComponent<AudioSource> ();
							ObjectRotate.Play ();

						} else if (parent_machine.transform.localEulerAngles.y == 180){
							parent_machine.transform.localRotation = Quaternion.Euler (0, 0, 0);

							AudioSource ObjectRotate = parent_machine.GetComponent<AudioSource> ();
							ObjectRotate.Play ();
						}
					}
//					if (parent_machine.tag == "Wedge") {
//						RotateObject(parent_machine);
//					}
				}
			}
		}
	}

//	void RotateObject(GameObject parent_machine)
//	{
//
//		startDragDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parent_machine.transform.position;
//		initialRotation = parent_machine.transform.rotation;
//		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parent_machine.transform.position;
//		difference.Normalize();
//
//
//		float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
//		parent_machine.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - (90 + offset));
//
//
//	}


}
