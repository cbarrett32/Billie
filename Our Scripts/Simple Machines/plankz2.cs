using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plankz2 : MonoBehaviour {

	public bool collided2;
	public bool isGrounded;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y <= -1.0f) {
			isGrounded = true;
			collided2 = false;
		} 
		
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "canmovepulley") {
			collided2 = true;
		}
	}
		

	void OnTriggerExit2D (Collider2D coll) {
		if (coll.gameObject.tag == "canmovepulley") {
			collided2 = false;
		}
	}
}
