using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lvl4 : MonoBehaviour {
	HandleCursor cursor;
	Detect_click dc;

	lvl3 l3;
	LeaderboardTestGUI lb;
	public GameObject[] starObjects;
	public int par4 = 2;
	Text txt;
	public int score4 = 0;
	//public GameObject sound;
	AudioSource footsound;
	private GameObject block4;

	GameObject[] menuObjects;


	void Start () {
		menuObjects = GameObject.FindGameObjectsWithTag("MenuButton");
		block4 = GameObject.Find ("blocker4");
		block4.SetActive (false);
		cursor = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<HandleCursor> ();
		dc = GameObject.Find ("GameObject_cursor").GetComponent<Detect_click> ();

		l3 = GameObject.Find ("CheckpointFlag3").GetComponent<lvl3> ();
		lb = GameObject.Find ("GUI").GetComponent<LeaderboardTestGUI> ();
		//sound = GameObject.Find ("Environmental Sound");
		footsound = GameObject.Find ("Billie").GetComponent<AudioSource> ();
		Time.timeScale = 1;
		starObjects = GameObject.FindGameObjectsWithTag("lvl4");
		txt = GameObject.Find("score_text4").GetComponent<Text>();
		hidestars();
	}

	void Update () {
		// blockers:
		if (PlayerPrefs.GetInt ("block4") == 1) {
			block4.SetActive (true);
		}
	}

	public void showstars() {
		// freeze menu buttons
		foreach (GameObject button in menuObjects)
		{
			button.GetComponent<Button>().interactable = false;
		}

		// change cursor back:
		cursor.setMouse ();
		dc.BackToDeselect ();

		AudioSource levelComplete = gameObject.GetComponent<AudioSource> ();
		levelComplete.Play ();

		//sound.SetActive (false);
		footsound.mute = true;
		int usage = PlayerPrefs.GetInt ("usage");
		txt.text = "Number of tools used: " + usage.ToString ();
		Time.timeScale = 0;
		if (usage <= par4) {
			score4 = 3;
			foreach (GameObject g in starObjects) {
				g.SetActive (true);
			}
		} else if (usage <= 2 * par4) {
			score4 = 2;
			foreach (GameObject g in starObjects) {
				if (g.name != "star3") {
					g.SetActive (true);
				}
			}
		} else {
			score4 = 1;
			foreach (GameObject g in starObjects) {
				if (g.name != "star3" && g.name != "star2") {
					g.SetActive (true);
				}
			}
		}
		score4 = score4 + PlayerPrefs.GetInt ("score3");
		PlayerPrefs.SetInt ("score4", score4);
		lb.savescore (score4);
	}
		

	public void hidestars() {
		// unfreeze menu buttons
		foreach (GameObject button in menuObjects)
		{
			button.GetComponent<Button>().interactable = true;
		}

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
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "Billie") {
			block4.SetActive (true);
			PlayerPrefs.SetInt ("block4", 1);

			float newx = other.gameObject.transform.position.x;
			float newy = other.gameObject.transform.position.y;
			float newz = other.gameObject.transform.position.z;
			float oldx = PlayerPrefs.GetFloat("PlayerX");
			// update last checked position of Billie
			if (newx > oldx) {
				PlayerPrefs.SetFloat ("PlayerX", newx + 5f);
				PlayerPrefs.SetFloat ("PlayerY", newy);
				PlayerPrefs.SetFloat ("PlayerZ", newz);
			}
		}
	}
}