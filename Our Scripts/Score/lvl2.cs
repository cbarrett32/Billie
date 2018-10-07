using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lvl2 : MonoBehaviour {
	HandleCursor cursor;
	Detect_click dc;

	LeaderboardTestGUI lb;
	lvl1 l1;
	public GameObject[] starObjects;
	public int par2 = 0;
	Text txt;
	public int score2 = 0;
	//public GameObject sound;
	AudioSource footsound;
	private GameObject block2;

	GameObject[] menuObjects;


	void Start () {
		menuObjects = GameObject.FindGameObjectsWithTag("MenuButton");
		block2 = GameObject.Find ("blocker2");
		block2.SetActive (false);
		//blocker surrounding level 2 is innitially inactive 
		cursor = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<HandleCursor> ();
		dc = GameObject.Find ("GameObject_cursor").GetComponent<Detect_click> ();
		l1 = GameObject.Find ("CheckpointFlag1").GetComponent<lvl1>();
		lb = GameObject.Find ("GUI").GetComponent<LeaderboardTestGUI> ();
		//sound = GameObject.Find ("Environmental Sound");
		footsound = GameObject.Find ("Billie").GetComponent<AudioSource> ();
		Time.timeScale = 1;
		starObjects = GameObject.FindGameObjectsWithTag("lvl2");
		txt = GameObject.Find("score_text2").GetComponent<Text>();
		hidestars();
		//initially hide the end of level pop up with stars
	}

	void Update () {
		// blockers:
		if (PlayerPrefs.GetInt ("block2") == 1) {
			block2.SetActive (true);
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
		//fingure out how many tools were used
		int usage = PlayerPrefs.GetInt ("usage");
		txt.text = "Number of tools used: " + usage.ToString ();
		Time.timeScale = 0;
		if (usage <= par2) {
			//if usage is <= par, you get 3 stars
			score2 = 3;
			foreach (GameObject g in starObjects) {
				g.SetActive (true);
			}
		} else if (usage <= 2 * par2) {
			// if usuage is less than 2 times the par, you get 2 stars
			score2 = 2;
			foreach (GameObject g in starObjects) {
				Debug.Log (g);
				if (g.name != "star3") {
					Debug.Log (g);
					g.SetActive (true);
				}
			}
		} else {
			// if usage is less than 3 times the par, you get 2 stars
			score2 = 1;
			foreach (GameObject g in starObjects) {
				if (g.name != "star3" && g.name != "star2") {
					g.SetActive (true);
				}
			}
		}
		score2 = score2 + PlayerPrefs.GetInt ("score1");
		PlayerPrefs.SetInt ("score2", score2);
		//save the score so far in player prefs for the leaderboard
		lb.savescore (score2);
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
		//hide the end of level pop up when you press "try again" or "next"
		foreach(GameObject g in starObjects){
			g.SetActive(false);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		//when you hit the flag, show the end of level pop up
		if (other.gameObject.name == "Billie") {
			showstars ();
		}
	}
	void OnTriggerExit2D(Collider2D other){
		// when you leave the flag, put up the blocker so that billie cant go back to the previous level
		if (other.gameObject.name == "Billie") {
			block2.SetActive (true);
			PlayerPrefs.SetInt ("block2", 1);

			float newx = other.gameObject.transform.position.x;
			float newy = other.gameObject.transform.position.y;
			float newz = other.gameObject.transform.position.z;
			float oldx = PlayerPrefs.GetFloat("PlayerX");
			// update last checked position of Billie
			if (newx > oldx) {
				PlayerPrefs.SetFloat ("PlayerX", newx + 10f);
				PlayerPrefs.SetFloat ("PlayerY", newy);
				PlayerPrefs.SetFloat ("PlayerZ", newz);
			}
		}
	}
}