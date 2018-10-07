using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_START : MonoBehaviour {
	bool unlocked;


	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Billie") {
			float newx = other.gameObject.transform.position.x;
			float newy = other.gameObject.transform.position.y;
			float newz = other.gameObject.transform.position.z;
			float oldx = PlayerPrefs.GetFloat("PlayerX");
			// update last checked position of Billie
			if (newx > oldx) {
				PlayerPrefs.SetFloat ("PlayerX", newx);
				PlayerPrefs.SetFloat ("PlayerY", newy);
				PlayerPrefs.SetFloat ("PlayerZ", newz);
			}

			// unlock audio
			if (!unlocked) {
				AudioSource itemUnlock = gameObject.GetComponent<AudioSource> ();
				itemUnlock.Play ();
				unlocked = true;
			}

		}
	}
}
