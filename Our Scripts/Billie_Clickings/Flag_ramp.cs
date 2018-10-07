using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag_ramp : MonoBehaviour {
	public Transform ramp_button;
	public Sprite ramp_sprite;
	private bool passed_ramp = false;
	int softreset;
	int flag;
	bool unlocked;
	GameObject panel2;
	GameObject help_panel_ramp;


	// Use this for initialization
	void Start () {
		panel2 = GameObject.Find ("help_panel2");
		panel2.SetActive (false);
		help_panel_ramp = GameObject.Find ("help_panel_ramp");
		help_panel_ramp.SetActive (false);
		// 1: has soft reset; 0: new game
		softreset = PlayerPrefs.GetInt("reset");
	}

	// Update is called once per frame
	void Update () {
		
		flag = PlayerPrefs.GetInt("flag");

		if (passed_ramp == false && softreset == 0) {
			ramp_button.GetComponent<Button> ().interactable = false;
		}  

		else if (flag >= 3){
			enable (ramp_button, ramp_sprite);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Billie") {
			float newx = other.gameObject.transform.position.x;
			float newy = other.gameObject.transform.position.y;
			float newz = other.gameObject.transform.position.z;
			float oldx = PlayerPrefs.GetFloat("PlayerX");
			if (newx > oldx) {
				PlayerPrefs.SetFloat ("PlayerX", newx);
				PlayerPrefs.SetFloat ("PlayerY", newy);
				PlayerPrefs.SetFloat ("PlayerZ", newz);
				PlayerPrefs.SetInt("flag", 3);
			}

			// unlock audio
			if (!unlocked) {
				AudioSource itemUnlock = gameObject.GetComponent<AudioSource> ();
				itemUnlock.Play ();
				unlocked = true;
			}

			passed_ramp = true;
			enable (ramp_button, ramp_sprite);

			// Destroy (panelFrog, 10.0f);

			int hasClosed2 = PlayerPrefs.GetInt ("closed2");
			if (hasClosed2 == 0) {
				if (panel2 != null) {
					// show helpbox
					panel2.SetActive (true);
					float ttl = 13.0f;
					Destroy (panel2, ttl);
					help_panel_ramp.SetActive (true);
					Destroy (help_panel_ramp, 4f);
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
