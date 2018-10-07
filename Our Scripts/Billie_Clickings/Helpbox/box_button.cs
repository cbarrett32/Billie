using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void click_and_close1(string variable) {
		// once closed, will never show up again
		PlayerPrefs.SetInt (variable, 1);
		// close the panel
		Destroy(transform.parent.gameObject);
	}
}
