using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {
	GameObject[] pauseObjects;
	GameObject sound;
	// Use this for initialization
	void Start () {
		sound = GameObject.Find ("Environmental Sound");
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidepaused ();

	}

	// Update is called once per frame
	void Update () {

	}

	public void showpaused() {
		sound.SetActive (false);
		Time.timeScale = 0;
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	public void hidepaused() {
		sound.SetActive (true);
		foreach(GameObject g in pauseObjects){
			Time.timeScale = 1;
			g.SetActive(false);
		}
	}
		
		
}
