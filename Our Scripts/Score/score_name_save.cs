using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class score_name_save : MonoBehaviour
{
	public string _scoreInput = "0";
	Text IO_name;

	void Start() {
		IO_name = GameObject.Find ("IO_name").GetComponent<Text>();
	}

	void Update() {

	}

	public void loadgame()
	{
		SceneManager.LoadScene("Level One 1");
	}
	public void loadscores()
	{
		SceneManager.LoadScene("scores");
	}
	public void loadscoreinput()
	{
		SceneManager.LoadScene("scoreinput");
	}
	public void loadmainmenu() 
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadCutScene() 
	{
		PlayerPrefs.SetString ("player_name", IO_name.text);
		SceneManager.LoadScene("Story Cutscene");
	}

	public void SaveAndPlay() {
		PlayerPrefs.SetString ("player_name", IO_name.text);
		//Leaderboard.Record (IO_name.text, 0);

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

		SceneManager.LoadScene("Story Cutscene");
	}
}