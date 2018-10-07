using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag_lever : MonoBehaviour {
	public Transform lever_button;
	public Sprite lever_sprite;
	private bool passed_lever = false;
	int softreset;
	int flag;
	bool unlocked;
	GameObject panel4;

	// Use this for initialization
	void Start () {
		// 1: has soft reset; 0: new game
		softreset = PlayerPrefs.GetInt("reset");
		panel4 = GameObject.Find ("help_panel4");
		panel4.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		
		flag = PlayerPrefs.GetInt("flag");

		if (passed_lever == false && softreset == 0)
		{
			//don't allow the button to be used
			lever_button.GetComponent<Button>().interactable = false;
		}

		else if (flag >= 4) {
			enable (lever_button, lever_sprite);
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
				PlayerPrefs.SetInt("flag", 4);
			}

			// unlock audio
			if (!unlocked) {
				AudioSource itemUnlock = gameObject.GetComponent<AudioSource> ();
				itemUnlock.Play ();
				unlocked = true;
			}


			passed_lever = true;;
			enable (lever_button, lever_sprite);

			int hasClosed4 = PlayerPrefs.GetInt ("closed4");
			if (hasClosed4 == 0) {
				if (panel4 != null) {
					// show helpbox
					panel4.SetActive (true);
					float ttl = 4.0f;
					Destroy (panel4, ttl);
				}
			}
		}
	}

	private void enable(Transform trans, Sprite sprite) {
		trans.GetComponent<Button>().interactable = true;
		trans.GetComponent<Image> ().sprite = sprite;
		trans.GetComponent<Image> ().SetNativeSize ();
		trans.GetComponent<RectTransform> ().localScale = new Vector3 (0.05f, 0.05f, 0.05f);
		// could potentionally change trans.GetComponent<RectTransform> ().position
	}
}
