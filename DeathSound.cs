using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour {
	AudioSource deathSound;

	// Use this for initialization
	void Start () {
		deathSound = this.gameObject.GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {

	}
	public void playSound()
	{
		deathSound.Play ();
	}
}
