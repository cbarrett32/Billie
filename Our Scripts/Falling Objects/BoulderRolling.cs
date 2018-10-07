using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRolling : MonoBehaviour {
	private Vector3 position; 
	private Quaternion rotation;

	// Use this for initialization
	void Start () {
		position = GetComponentInParent<Transform> ().position;
		rotation = GetComponentInParent<Transform> ().rotation;

	}
	
	// Update is called once per frame
	void Update () {
		//transform.GetComponent<Rigidbody2D>().constraints.
		//transform.position = GetComponentInParent<Transform> ().position;
		//transform.Translate (Vector3.up * -4.7f);
			//new Vector3 (position.x, position.y, 0);
		//transform.rotation = rotation;
	}
}
