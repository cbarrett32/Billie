using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardTestGUI : MonoBehaviour {
	public string _nameInput;
	public string _scoreInput = "0";

	void Start() {
	}

	void Update() {
		_nameInput = PlayerPrefs.GetString("player_name");
	}

	/*
	private void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width/3, Screen.height/2, Screen.width/2, Screen.height));

		// Display high scores!
//		for (int i = 0; i < Leaderboard.EntryCount; ++i) {
//			var entry = Leaderboard.GetEntry(i);
//			GUILayout.Label("Name: " + entry.name + ", Score: " + entry.score);
//		}

		// Interface for reporting test scores.
		GUI.skin.label.fontSize = 40;
		GUILayout.Space(10);

		_nameInput = GUILayout.TextField(_nameInput);
//		_scoreInput = GUILayout.TextField(_scoreInput);

		if (GUILayout.Button("Submit")) {
			enabled = false;
//			int score;
//			int.TryParse(_scoreInput, out score);
//
//			Leaderboard.Record(_nameInput, score);
//
//			// Reset for next input.
//			_nameInput = "";
//			_scoreInput = "0";
		}

		GUILayout.EndArea();
	}
	*/

	public void savescore(int score) {
		Leaderboard.Record(_nameInput, score);
	}

	public void Reset() {
		Leaderboard.ResetScore ();
	}


}