using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertedSound : MonoBehaviour {
	AudioSource bugleCall;

	// Use this for initialization
	void Start () {
		bugleCall = this.gameObject.GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {

	}
	public void playSound()
	{
		bugleCall.Play ();
	}
}
