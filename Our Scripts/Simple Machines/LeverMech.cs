using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverMech: MonoBehaviour {

	//make a list to track collided objects
	List<Collider2D> collidedObjects = new List<Collider2D>();
	//the obect to be launched
	public GameObject launchee;
	public Rigidbody2D rb;
	//public float force; //set velocity of launch
	public Vector2 position;
	//script used to attach force to the launchee
	private ForceExperiments blowUp;
	private Transform fulcrum;
	private lever rocksLever;

	private bool stopExit =false;

	private bool applyItOnce = true;

	private Collision2D outsideColl;

	private FixedJoint2D tempjoint;


	void Awake() {
		fulcrum = GetComponentInChildren<Transform>();
		launchee = null;
		//Debug.Log (fulcrum.position);
	}

	void FixedUpdate() {
		// clear the launchee
		//launchee = null;
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (launchee != null) {
			if (coll.gameObject.tag == "canmovepulley") {
				//Debug.Log("Correct location");
				//Debug.Log ("Collision script: " + coll.gameObject);
					outsideColl = coll;
					tempjoint = coll.gameObject.AddComponent<FixedJoint2D> ();
					tempjoint.connectedBody = gameObject.GetComponent<Rigidbody2D> ();

			}
//			if (launchee.name != "Billie") {
		//		stopExit = true;
			//}
		}
	}


	void OnTriggerEnter2D (Collider2D col) {
		if (outsideColl != null) {
				//Debug.Log ("OutsideColl not null: " + outsideColl.gameObject);
				Destroy (outsideColl.gameObject.GetComponent<FixedJoint2D> ());
		}
		GameObject launcher = col.gameObject;
		//if nothing has been identified as the launchee and the launcher isn't the launche(if billie walks across it), do nothing
		//Debug.Log("Trigger script. Launchee= " + launchee + "Launcher= " + launcher);
		if (launcher.tag == "simplemachine") {
			//Destroy (launcher);
		}
		if (launchee != null && launchee!= launcher && launcher.tag!="Bullet") {
			//if (launcher.name == "Billie" && launchee.tag == "canmovepulley") {
			//	launchee.transform.Translate 
			//}
			//else {
				rb.constraints = RigidbodyConstraints2D.None;
				blowUp = launchee.GetComponent<ForceExperiments> ();
				//Move the launchee up so it doesn't get affected by the lever sprite switch

			if (applyItOnce == true) {
				//applyItOnce = false;
				launchee.transform.Translate (Vector3.up * 7);
			} 
				//if the launchee is to the right of the fulcrum (child of the lever object), pass false to ForceExperiments
				//if to the left, pass true.
				//Debug.Log("Fulcrum pos: " + fulcrum.position.x);
				//Debug.Log("launchee x pos: " + launchee.transform.position.x);
				if (launchee.transform.position.x + 1.0f > fulcrum.position.x) {
					//if there is something to be launched, launch it
					if (applyItOnce == true) {
						blowUp.goFlying (false);
						applyItOnce = false;
					} 

					this.transform.localRotation = Quaternion.Euler (0, 180, 0);
				} else {
					if (applyItOnce == true) {
						blowUp.goFlying (true);
						applyItOnce = false;
					} 
					this.transform.localRotation = Quaternion.Euler (0, 0, 0);
				}
				//once launched, the lauchee is no longer the launchee
				//launchee = null;
				stopExit = false;
			//}
		}



			
	}
		
	void OnTriggerStay2D (Collider2D col) {
		// if something collides and stays, mark it as a potential thing to be launched (launchee)
		if (col.gameObject.tag == "canmovepulley"||col.gameObject.tag=="Player") {
			launchee = col.gameObject;
			//Debug.Log ("Launchee: " + col.gameObject.name);
			rocksLever = launchee.GetComponent<lever> ();
			rb=launchee.GetComponent<Rigidbody2D> ();
		}
		//working on this

	}
	void OnTriggerExit2D (Collider2D collider){
	//when it goes away, clear the launchee
		if (collider.gameObject.name=="Billie") {
			//Debug.Log ("trigger exit");

			launchee = null;
			rb = null;
		}
	}



}
