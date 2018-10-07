using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){       
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

			if(hit.collider != null)
			{
				Debug.Log ("Target Position: " + hit.collider.gameObject.name);
				GameObject.Find (hit.collider.gameObject.name).SetActive (false);
			}
		}
	}
}
