using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider2D))]
public class cbDestroyableScript : MonoBehaviour {

	private bool cutRope=false;
	private RopeSound ropesound;
	public GameObject placeHere;


	// Use this for initialization
	void Start () {
		ropesound = placeHere.GetComponent<RopeSound> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
//		Debug.Log (collider.gameObject.name);
		if (collider.tag == "Bullet") {
			ropesound.playSound ();
			Destroy (gameObject);
			GameObject go = collider.gameObject;
			Destroy (go);
		}
	}


}
