using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnaptoGrid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var currentPos = transform.position;
		transform.position = new Vector2(Mathf.Round(currentPos.x),Mathf.Round(currentPos.y));
	}

	// Update is called once per frame
	void Update () {
	}

}
