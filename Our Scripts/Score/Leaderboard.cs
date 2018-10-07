using UnityEngine;

using System.Collections.Generic;

public static class Leaderboard {
	public const int EntryCount = 10;

	public struct ScoreEntry {
		public string name;
		public int score;

		public ScoreEntry(string name, int score) {
			this.name = name;
			this.score = score;
		}
	}

	public static List<ScoreEntry> s_Entries;

	public static List<ScoreEntry> Entries {
		get {
			if (s_Entries == null) {
				s_Entries = new List<ScoreEntry>();
				LoadScores();
			}
			return s_Entries;
		}
	}

	public const string PlayerPrefsBaseKey = "leaderboard";

	public static void SortScores() {
		s_Entries.Sort((a, b) => b.score.CompareTo(a.score));
	}

	public static void LoadScores() {
		s_Entries.Clear();

		for (int i = 0; i < EntryCount; ++i) {
			ScoreEntry entry;
			entry.name = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + i + "].name", "???");
			entry.score = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + i + "].score", 0);
			s_Entries.Add(entry);
		}

		SortScores();
	}

	public static void SaveScores() {
		for (int i = 0; i < EntryCount; ++i) {
			var entry = s_Entries[i];
			PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", entry.name);
			PlayerPrefs.SetInt(PlayerPrefsBaseKey + "[" + i + "].score", entry.score);
		}
	}

	public static ScoreEntry GetEntry(int index) {
		return Entries[index];
	}

	public static void Record(string name, int score) {
		for (int i = 0; i < EntryCount; ++i) {
			if (Entries[i].name == name) {
				PlayerPrefs.SetString (PlayerPrefsBaseKey + "[" + i + "].name", "???");
				PlayerPrefs.SetInt (PlayerPrefsBaseKey + "[" + i + "].score", 0);
				if (score >= Entries [i].score) {
					Entries.RemoveAt (i);
					Entries.Add (new ScoreEntry ("???", 0));
				}
			}
		}
		Entries.Add(new ScoreEntry(name, score));
		SortScores();
		Entries.RemoveAt(Entries.Count - 1);
		SaveScores();

		for (int i = 0; i < EntryCount; ++i) {
			var entry = Entries[i];
//			Debug.Log (i);
//			Debug.Log (PlayerPrefs.GetString (PlayerPrefsBaseKey + "[" + i + "].name"));
//			Debug.Log (PlayerPrefs.GetInt (PlayerPrefsBaseKey + "[" + i + "].score"));
		}
	}

	public static void ResetScore() {
		for (int i = 0; i < EntryCount; ++i) {
			// Debug.Log (PlayerPrefs.GetString (PlayerPrefsBaseKey + "[" + i + "].name"));
			PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", "???");
			PlayerPrefs.SetInt(PlayerPrefsBaseKey + "[" + i + "].score", 0);
			Entries.RemoveAt (0);
			Entries.Add (new ScoreEntry ("???", 0));
			SortScores();

//			Debug.Log (i);
//			Debug.Log (PlayerPrefs.GetString (PlayerPrefsBaseKey + "[" + i + "].name"));
		}
	}
}