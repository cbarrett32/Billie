using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectTowards_wedge : MonoBehaviour {
	public float offset = 180.0f;

	Vector3 startDragDir;
	Vector3 currentDragDir;
	Quaternion initialRotation;
	float angleFromStart;

	void OnMouseDown(){

		startDragDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		initialRotation = transform.rotation;
	}


	void OnMouseDrag(){
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		difference.Normalize();


		float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - (90 + offset));
	}


}
