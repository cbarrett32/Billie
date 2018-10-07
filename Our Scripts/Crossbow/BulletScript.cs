using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	private SpriteRenderer m_SpriteRenderer;
	private bool hasBeenSeen = false;


	void Awake () {
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}
	// Use this for initialization
	void Update() {
		if (m_SpriteRenderer.isVisible == true) {
			hasBeenSeen = true;
		}
		if (m_SpriteRenderer.isVisible == false && hasBeenSeen==true) {
			Destroy (gameObject);
		}
	}

	void OnBecomeInvisible() {
		Destroy (gameObject);
	}


}
