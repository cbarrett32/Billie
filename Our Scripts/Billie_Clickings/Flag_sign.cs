using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_sign : MonoBehaviour {
	GameObject help_reset;

	// Use this for initialization
	void Start () {
		help_reset = GameObject.Find ("help_reset");
		help_reset.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Billie") {
			if (transform.name == "Retry_flag") {
				if (help_reset != null) {
					help_reset.SetActive (true);
					Destroy (help_reset, 8.0f);
				}
			}
		}
	}
}
