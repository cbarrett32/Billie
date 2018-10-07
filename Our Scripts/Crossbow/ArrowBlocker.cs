using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBlocker : MonoBehaviour {

	// Use this for initialization
	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Bullet") {
			//Ryan, tink Sound goes here
			GameObject go = collider.gameObject;
			Destroy (go);
		}
	}

}
