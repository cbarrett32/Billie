using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwarenessSound : MonoBehaviour {
	AudioSource awarenessMeter;

	// Use this for initialization
	void Start () {
		awarenessMeter = this.gameObject.GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {

	}
	public void playSound()
	{
		awarenessMeter.Play ();
	}
	public void stopSound()
	{
		awarenessMeter.Stop ();
	}
}
