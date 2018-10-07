using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(Collider2D))]
public class WedgePlank : MonoBehaviour {

	private WedgeSound wedgeSound;


	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
		wedgeSound = GameObject.Find("WedgeSound").GetComponent<WedgeSound> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	//If the wedge touches the plank, destroy both of them.
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Wedge") {
			wedgeSound.playSound ();
			Destroy (gameObject);
			GameObject go = collider.gameObject;
			Destroy (go);
		} else {
			//gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
		}

	}

}
