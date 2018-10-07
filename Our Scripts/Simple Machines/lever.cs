using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour {
	
	private SpriteRenderer spriteRenderer; 

	private Quaternion zero;
	private Quaternion oneEighty;

	private bool onlyRotateOnce=true;
	private bool repeatImpossible=true;
	private string myName;

	private LeverMech theLever;
	private GameObject launchee;

	void Awake() {
		//zero = Quaternion.Euler (0, 0, 0);
		//oneEighty = Quaternion.Euler(0.0f, 180.0f, 0.0f);
		myName = gameObject.name;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		spriteRenderer = other.GetComponent<SpriteRenderer>();

		if (other is EdgeCollider2D)
		{
			theLever = other.GetComponent<LeverMech> ();
			if (theLever.launchee != null) {
				//Debug.Log (theLever.launchee.name);
				launchee = theLever.launchee;
			}
//			Debug.Log (theLever.launchee.name);

			//Debug.Log ("Should Rotate, angle is: " + other.transform.localEulerAngles.y);

			if(onlyRotateOnce || myName == "Billie") 
			{
				if (other.transform.localEulerAngles.y == 0) {
					onlyRotateOnce = false;
					if (launchee == null) {
						onlyRotateOnce = false;
						//Debug.Log ("launchee is null");
					} else if (launchee.name == "Billie") {
						onlyRotateOnce = true;
						theLever.launchee = null;
					}
					if (repeatImpossible) {
						other.transform.localRotation = Quaternion.Euler (0, 180, 0);
						repeatImpossible = false;
					} else {
						other.transform.localRotation = Quaternion.Euler (0, 0, 0);
					}
						
				} else if (other.transform.localEulerAngles.y == 180){
					other.transform.localRotation = Quaternion.Euler (0, 0, 0);
					onlyRotateOnce = false;
					repeatImpossible = true;

				}
			}
		}


	}

}