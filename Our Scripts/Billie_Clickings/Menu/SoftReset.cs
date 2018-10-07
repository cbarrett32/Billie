using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoftReset : MonoBehaviour {
	GameObject Billie;
	GameObject keyDirections;
	Text usage_text;
	int reset = 0;

	// Use this for initialization
	void Start () {
		Billie = GameObject.Find ("Billie");
		keyDirections = GameObject.Find ("keyboard_instc");
		usage_text = GameObject.Find ("usage_text").GetComponent<Text>();
		reset = PlayerPrefs.GetInt ("reset");

		if (reset == 1) {
			float newX = PlayerPrefs.GetFloat ("PlayerX");
			float newY = PlayerPrefs.GetFloat ("PlayerY"); 
			float newZ = PlayerPrefs.GetFloat ("PlayerZ");
			Billie.transform.position = new Vector3 (newX, newY, newZ);
			keyDirections.SetActive (false);
		} else {
			keyDirections.SetActive (true);
			Destroy (keyDirections, 4f);
		}



	}
	
	// Update is called once per frame
	void Update () {
		int usage = PlayerPrefs.GetInt ("usage");
		usage_text.text = "Tools used: " + usage.ToString();
	}

	public void LoadSceneOnClick(int sceneNo)
	{

		PlayerPrefs.SetInt("reset", 1);
		PlayerPrefs.SetInt ("usage", 0);
		SceneManager.LoadScene(sceneNo);

	}

	public void HardReset(int sceneNo)
	{
		PlayerPrefs.SetInt("flag", 0);
		PlayerPrefs.SetInt ("closed1", 0);
		PlayerPrefs.SetInt ("closed2", 0);
		PlayerPrefs.SetInt ("closed_rotate", 0);
		PlayerPrefs.SetInt ("closed4", 0);
		PlayerPrefs.SetInt ("closed5", 0);
		PlayerPrefs.SetInt ("closed6", 0);
		PlayerPrefs.SetInt("reset", 0);
		PlayerPrefs.SetInt ("usage", 0);
		PlayerPrefs.SetFloat ("PlayerX", 0.0f);
		PlayerPrefs.SetFloat ("PlayerY", 0.0f);
		PlayerPrefs.SetFloat ("PlayerZ", 0.0f);

		PlayerPrefs.SetInt ("block1", 0);
		PlayerPrefs.SetInt ("block2", 0);
		PlayerPrefs.SetInt ("block3", 0);
		PlayerPrefs.SetInt ("block4", 0);
		PlayerPrefs.SetInt ("block5", 0);
		PlayerPrefs.SetInt ("block6", 0);
		PlayerPrefs.SetInt ("block7", 0);
		PlayerPrefs.SetInt ("block8", 0);


		PlayerPrefs.SetInt ("score1", 0);
		PlayerPrefs.SetInt ("score2", 0);
		PlayerPrefs.SetInt ("score3", 0);
		PlayerPrefs.SetInt ("score4", 0);
		PlayerPrefs.SetInt ("score5", 0);
		PlayerPrefs.SetInt ("score6", 0);
		PlayerPrefs.SetInt ("score7", 0);
		PlayerPrefs.SetInt ("score8", 0);
		PlayerPrefs.SetInt ("score9", 0);

		SceneManager.LoadScene(sceneNo);

	}

	public void loadmainmenu() 
	{
		SceneManager.LoadScene("MainMenu");
	}
}
