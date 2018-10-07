using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

	JumpingCharacterController jcc;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
		jcc = GetComponent<JumpingCharacterController> ();
		audioSource = GetComponent<AudioSource> ();
	
	}

	// Update is called once per frame
	void Update () {
			//If Billie is moving and on the ground, play foostep sound
			if (jcc.isGrounded == true && (jcc.vSpeed > .01f || jcc.vSpeed < -.01f) && !audioSource.isPlaying) {

				audioSource.volume = Random.Range (0.05f, 0.1f);
				audioSource.pitch = Random.Range (0.9f, 1.1f);
				audioSource.Play ();
			}

	}
}