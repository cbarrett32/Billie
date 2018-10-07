using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_sign_2 : MonoBehaviour {
	GameObject help_panel_frog;

	// Use this for initialization
	void Start () {

		help_panel_frog = GameObject.Find ("help_panel_frog");
		help_panel_frog.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Billie") {

			if (transform.name == "Enemy_flag") {
				if (help_panel_frog != null) {
					help_panel_frog.SetActive (true);
					Destroy (help_panel_frog, 8.0f);
				}
			}
		}
	}
}
