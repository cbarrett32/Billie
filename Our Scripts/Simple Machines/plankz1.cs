using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plankz1 : MonoBehaviour {

	public bool collided1;
	public bool isGrounded;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y <= -1.0f) {
			isGrounded = true;
			collided1 = false;
		} 

	}
		
	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "canmovepulley") {
			collided1 = true;
		}
	}

	void OnTriggerExit2D (Collider2D coll) {
		if (coll.gameObject.tag == "canmovepulley") {
			collided1 = false;
		}
	}

}
