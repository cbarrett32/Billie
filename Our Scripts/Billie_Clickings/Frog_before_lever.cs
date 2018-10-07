using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_before_lever : MonoBehaviour {
	int flag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		flag = PlayerPrefs.GetInt("flag");
		if (flag >= 3) {
			//gameObject.SetActive (false);
		}
	}
}
