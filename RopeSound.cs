using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSound : MonoBehaviour {
	AudioSource ropeSnap;

	// Use this for initialization
	void Start () {
		ropeSnap = this.gameObject.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void playSound()
	{
		ropeSnap.Play ();
	}
}
