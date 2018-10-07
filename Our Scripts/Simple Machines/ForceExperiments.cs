using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceExperiments : MonoBehaviour {

	private Rigidbody2D rb;
	//public variable to determine how far up and right the lever sends you
	public float howFarUp;
	public float howFarRight;
	private Vector2 force; //set velocity of launch
	[HideInInspector]
	public bool hasBeenShot= false;

	void Awake(){
		//force = new Vector2 (howFarRight, howFarUp);
	}

	//take a boolean to determine whether or not you're shot to the right or left
	public void goFlying(bool fliesRight)
	{
		hasBeenShot = true;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		if (fliesRight) {
			force = new Vector2 (howFarRight, howFarUp);
			//Debug.Log ("Flies Right" + howFarUp + " " + howFarUp);
		}
		else {
			force = new Vector2 (-howFarRight, howFarUp);
			//Debug.Log ("Flies Left" + howFarUp + " " + howFarUp);

		}
		//after declaring the force, addforce to launchee's rigidbody
		rb.AddForce (force, ForceMode2D.Impulse);


	}

}
