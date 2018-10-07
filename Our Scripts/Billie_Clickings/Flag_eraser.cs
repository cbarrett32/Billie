using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag_eraser : MonoBehaviour {
	public Transform eraser_button;
	public Sprite eraser_sprite;
	private bool passed_eraser = false;
	int softreset;
	int flag;
	bool unlocked;
	GameObject panel_eraser;
	GameObject help_panel_eraser;

	// Use this for initialization
	void Start () {
		panel_eraser = GameObject.Find ("help_panel1.5");
		panel_eraser.SetActive (false);

		help_panel_eraser = GameObject.Find ("help_panel_eraser");
		help_panel_eraser.SetActive (false);
		// 1: has soft reset; 0: new game
		softreset = PlayerPrefs.GetInt("reset");
	}

	// Update is called once per frame
	void Update () {

		flag = PlayerPrefs.GetInt("flag");

		if (passed_eraser == false && softreset == 0) {
			eraser_button.GetComponent<Button> ().interactable = false;
		}  

		else if (flag >= 2){
			enable (eraser_button, eraser_sprite);
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
				PlayerPrefs.SetInt("flag", 2);
			}

			// unlock audio
			if (!unlocked) {
				AudioSource itemUnlock = gameObject.GetComponent<AudioSource> ();
				itemUnlock.Play ();
				unlocked = true;
			}

			passed_eraser = true;
			enable (eraser_button, eraser_sprite);

			// Destroy (panelFrog, 10.0f);

			if (panel_eraser != null) {
				// show helpbox
				panel_eraser.SetActive (true);
				float ttl = 8.0f;
				Destroy (panel_eraser, ttl);
			}

			if (help_panel_eraser != null) {
				help_panel_eraser.SetActive (true);
				Destroy (help_panel_eraser, 8.0f);
			}


		}
	}

	private void enable(Transform trans, Sprite sprite) {
		trans.GetComponent<Button>().interactable = true;
		trans.GetComponent<Image> ().sprite = sprite;
		trans.GetComponent<Image> ().SetNativeSize ();
	}
}
