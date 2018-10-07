using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag_rope : MonoBehaviour {
	public Transform rope_button;
	public Sprite rope_sprite;
	private bool passed_rope = false;
	int softreset;
	int flag;
	bool unlocked;
	GameObject panel5;

	// Use this for initialization
	void Start () {
		panel5 = GameObject.Find ("help_panel5");
		panel5.SetActive (false);
		// 1: has soft reset; 0: new game
		softreset = PlayerPrefs.GetInt("reset");
	}

	// Update is called once per frame
	void Update () {
		flag = PlayerPrefs.GetInt("flag");

		if (passed_rope == false && softreset == 0) {
			//don't allow the button to be used
			rope_button.GetComponent<Button> ().interactable = false;
		} 

		else if (flag >= 5) {
			enable (rope_button, rope_sprite);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Billie") {
			float newx = other.gameObject.transform.position.x;
			float newy = other.gameObject.transform.position.y;
			float newz = other.gameObject.transform.position.z;
			float oldx = PlayerPrefs.GetFloat("PlayerX");
			// update last checked position of Billie
			if (newx > oldx) {
				PlayerPrefs.SetFloat ("PlayerX", newx);
				PlayerPrefs.SetFloat ("PlayerY", newy);
				PlayerPrefs.SetFloat ("PlayerZ", newz);
				PlayerPrefs.SetInt("flag", 5);
			}

			// unlock audio
			if (!unlocked) {
				AudioSource itemUnlock = gameObject.GetComponent<AudioSource> ();
				itemUnlock.Play ();
				unlocked = true;
			}

			passed_rope = true;;
			enable (rope_button, rope_sprite);

			int hasClosed5 = PlayerPrefs.GetInt ("closed5");
			if (hasClosed5 == 0) {
				if (panel5 != null) {
					// show helpbox
					panel5.SetActive (true);
					float ttl = 4.0f;
					Destroy (panel5, ttl);
				}
			}
		}
	}

	private void enable(Transform trans, Sprite sprite) {
		trans.GetComponent<Button>().interactable = true;
		trans.GetComponent<Image> ().sprite = sprite;
		trans.GetComponent<Image> ().SetNativeSize ();
	}
}
