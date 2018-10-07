using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag_wedge : MonoBehaviour {
	public Transform wedge_button;
	public Sprite wedge_sprite;
	private bool passed_wedge = false;
	int softreset;
	int flag;
	bool unlocked;
	GameObject panel6;

	// Use this for initialization
	void Start () {
		// 1: has soft reset; 0: new game
		softreset = PlayerPrefs.GetInt("reset");
		panel6 = GameObject.Find ("help_panel6");
		panel6.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

		flag = PlayerPrefs.GetInt("flag");

		if (passed_wedge == false && softreset == 0)
		{
			//don't allow the button to be used
			wedge_button.GetComponent<Button>().interactable = false;
		}

		else if (flag >= 6) {
			enable (wedge_button, wedge_sprite);
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
				PlayerPrefs.SetInt("flag", 6);
			}

			// unlock audio
			if (!unlocked) {
				AudioSource itemUnlock = gameObject.GetComponent<AudioSource> ();
				itemUnlock.Play ();
				unlocked = true;
			}


			passed_wedge = true;;
			enable (wedge_button, wedge_sprite);

			int hasClosed6 = PlayerPrefs.GetInt ("closed6");
			if (hasClosed6 == 0) {
				if (panel6 != null) {
					// show helpbox
					panel6.SetActive (true);
					float ttl = 4.0f;
					Destroy (panel6, ttl);
				}
			}
		}
	}

	private void enable(Transform trans, Sprite sprite) {
		trans.GetComponent<Button>().interactable = true;
		trans.GetComponent<Image> ().sprite = sprite;
		trans.GetComponent<Image> ().SetNativeSize ();
		// could potentionally change trans.GetComponent<RectTransform> ().position
	}
}
