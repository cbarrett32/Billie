using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lvl11 : MonoBehaviour {
	HandleCursor cursor;
	Detect_click dc;

	lvl10 l10;
	LeaderboardTestGUI lb;
	public GameObject[] starObjects;
	public int par11 = 1;
	Text txt;
	public int score11 = 0;
	//public GameObject sound;
	AudioSource footsound;

	void Start () {
		cursor = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<HandleCursor> ();
		dc = GameObject.Find ("GameObject_cursor").GetComponent<Detect_click> ();

		l10 = GameObject.Find ("CheckpointFlag10").GetComponent<lvl10> ();
		lb = GameObject.Find ("GUI").GetComponent<LeaderboardTestGUI> ();
		//sound = GameObject.Find ("Environmental Sound");
		footsound = GameObject.Find ("Billie").GetComponent<AudioSource> ();
		Time.timeScale = 1;
		starObjects = GameObject.FindGameObjectsWithTag("lvl11");
		txt = GameObject.Find("score_text11").GetComponent<Text>();
		hidestars();
	}

	void Update () {
	}

	public void showstars() {
		// change cursor back:
		cursor.setMouse ();
		dc.BackToDeselect ();

		//sound.SetActive (false);
		footsound.mute = true;
		int usage = PlayerPrefs.GetInt ("usage");
		txt.text = "Number of tools used: " + usage.ToString ();
		Time.timeScale = 0;
		if (usage <= par11) {
			score11 = 3;
			foreach (GameObject g in starObjects) {
				g.SetActive (true);
			}
		} else if (usage <= 2 * par11) {
			score11 = 2;
			foreach (GameObject g in starObjects) {
				if (g.name != "star3") {
					g.SetActive (true);
				}
			}
		} else {
			score11 = 1;
			foreach (GameObject g in starObjects) {
				if (g.name != "star3" && g.name != "star2") {
					g.SetActive (true);
				}
			}
		}
		score11 = score11 + l10.score10;
		lb.savescore (score11);
	}
		

	public void hidestars() {
		//sound.SetActive (true);
		footsound.mute = false;
		PlayerPrefs.SetInt ("usage", 0);
		Time.timeScale = 1;
		foreach(GameObject g in starObjects){
			g.SetActive(false);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Billie") {
			showstars ();
		}
//		if (other.gameObject.name == "Billie") {
//			float newx = other.gameObject.transform.position.x;
//			float newy = other.gameObject.transform.position.y;
//			float newz = other.gameObject.transform.position.z;
//			float oldx = PlayerPrefs.GetFloat ("PlayerX");
//			if (newx > oldx) {
//				PlayerPrefs.SetFloat ("PlayerX", newx);
//				PlayerPrefs.SetFloat ("PlayerY", newy);
//				PlayerPrefs.SetFloat ("PlayerZ", newz);
//			}
//		}
	}
}