using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag_crossbow : MonoBehaviour {
	public Transform crossbow_button;
	public Sprite crossbow_sprite;
	private bool passed_crossbow = false;
	int softreset;
	int flag;
	bool unlocked;
	GameObject panel1;
	GameObject panel_reset;

	// Use this for initialization
	void Start () {
		panel1 = GameObject.Find ("help_panel1");
		panel1.SetActive (false);
		// 1: has soft reset; 0: new game
		softreset = PlayerPrefs.GetInt("reset");
	}

	// Update is called once per frame
	void Update () {
		flag = PlayerPrefs.GetInt("flag");

		if (passed_crossbow == false && softreset == 0) {
			//don't allow the button to be used
			crossbow_button.GetComponent<Button> ().interactable = false;
		} 

		else if (flag >= 1) {
			enable_crossbow ();
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
				PlayerPrefs.SetInt("flag", 1);
			}

			// unlock audio
			if (!unlocked) {
				AudioSource itemUnlock = gameObject.GetComponent<AudioSource> ();
				itemUnlock.Play ();
				unlocked = true;
			}

			passed_crossbow = true;;
			enable_crossbow ();

			int hasClosed1 = PlayerPrefs.GetInt ("closed1");
			if (hasClosed1 == 0) {
				// show helpbox
				if (panel1 != null) {
					panel1.SetActive (true);
					float ttl = 8.0f;
					Destroy (panel1, ttl);
				}



			}
		}
	}

	public void enable_crossbow() {
		crossbow_button.GetComponent<Button>().interactable = true;
		crossbow_button.GetComponent<Image> ().sprite = crossbow_sprite;
		crossbow_button.GetComponent<Image> ().SetNativeSize ();
	}
}
