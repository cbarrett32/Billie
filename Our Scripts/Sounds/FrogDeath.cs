using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDeath : MonoBehaviour {
	AudioSource frogDeath;

	// Use this for initialization
	void Start () {
		frogDeath = this.gameObject.GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {

	}
	public void playSound()
	{
		frogDeath.Play ();
	}
}
