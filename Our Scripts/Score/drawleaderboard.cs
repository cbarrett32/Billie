using UnityEngine;
using UnityEngine.UI;

public class drawleaderboard : MonoBehaviour {
//	public string _nameInput = "Please enter your name...";
//	public string _scoreInput = "0";

	Text n1;
	Text s1;

	Text n2;
	Text s2;

	Text n3;
	Text s3;

	Text n4;
	Text s4;

	Text n5;
	Text s5;

	Text n6;
	Text s6;

	Text n7;
	Text s7;

	Text n8;
	Text s8;

	Text n9;
	Text s9;

	Text n10;
	Text s10;

	private const string PlayerPrefsBaseKey = "leaderboard";

	void Start() {
		n1 = GameObject.Find ("n1").GetComponent<Text>();
		s1 = GameObject.Find ("s1").GetComponent<Text>();

		n2 = GameObject.Find ("n2").GetComponent<Text>();
		s2 = GameObject.Find ("s2").GetComponent<Text>();

		n3 = GameObject.Find ("n3").GetComponent<Text>();
		s3 = GameObject.Find ("s3").GetComponent<Text>();

		n4 = GameObject.Find ("n4").GetComponent<Text>();
		s4 = GameObject.Find ("s4").GetComponent<Text>();

		n5 = GameObject.Find ("n5").GetComponent<Text>();
		s5 = GameObject.Find ("s5").GetComponent<Text>();

		n6 = GameObject.Find ("n6").GetComponent<Text>();
		s6 = GameObject.Find ("s6").GetComponent<Text>();

		n7 = GameObject.Find ("n7").GetComponent<Text>();
		s7 = GameObject.Find ("s7").GetComponent<Text>();

		n8 = GameObject.Find ("n8").GetComponent<Text>();
		s8 = GameObject.Find ("s8").GetComponent<Text>();

		n9 = GameObject.Find ("n9").GetComponent<Text>();
		s9 = GameObject.Find ("s9").GetComponent<Text>();

		n10 = GameObject.Find ("n10").GetComponent<Text>();
		s10 = GameObject.Find ("s10").GetComponent<Text>();
	}

	void Update() {

//		n1.text = Leaderboard.GetEntry (0).name;
//		s1.text = Leaderboard.GetEntry (0).score.ToString();
		
		n1.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 0 + "].name", "???");
		s1.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 0 + "].score", 0).ToString();

		n2.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 1 + "].name", "???");
		s2.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 1 + "].score", 0).ToString();

		n3.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 2 + "].name", "???");
		s3.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 2 + "].score", 0).ToString();

		n4.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 3 + "].name", "???");
		s4.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 3 + "].score", 0).ToString();

		n5.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 4 + "].name", "???");
		s5.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 4 + "].score", 0).ToString();

		n6.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 5 + "].name", "???");
		s6.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 5 + "].score", 0).ToString();

		n7.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 6 + "].name", "???");
		s7.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 6 + "].score", 0).ToString();

		n8.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 7 + "].name", "???");
		s8.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 7 + "].score", 0).ToString();

		n9.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 8 + "].name", "???");
		s9.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 8 + "].score", 0).ToString();

		n10.text = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + 9 + "].name", "???");
		s10.text = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + 9 + "].score", 0).ToString();

	}

	/*
	private void OnGUI() {
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));

		// Display high scores!
		for (int i = 0; i < Leaderboard.EntryCount; ++i) {
			var entry = Leaderboard.GetEntry (i);
			GUILayout.Label ("Name: " + entry.name + ", Score: " + entry.score);
		}

		// Interface for reporting test scores.
		GUILayout.Space (10);
	}
	*/



//		_nameInput = GUILayout.TextField(_nameInput);
//		//		_scoreInput = GUILayout.TextField(_scoreInput);
//
//		if (GUILayout.Button("Submit")) {
//			//			int score;
//			//			int.TryParse(_scoreInput, out score);
//			//
//			//			Leaderboard.Record(_nameInput, score);
//			//
//			//			// Reset for next input.
//			//			_nameInput = "";
//			//			_scoreInput = "0";
//		}
//
//		GUILayout.EndArea();
//	}
//
//	public void savescore(int score) {
//		Leaderboard.Record(_nameInput, score);
//	}
}