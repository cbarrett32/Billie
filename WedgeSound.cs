using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WedgeSound : MonoBehaviour {
	AudioSource wedgeSound;

	// Use this for initialization
	void Start () {
		wedgeSound = this.gameObject.GetComponent<AudioSource> ();


	}

	// Update is called once per frame
	void Update () {

	}
	public void playSound()
	{
		wedgeSound.Play ();
	}	
}
