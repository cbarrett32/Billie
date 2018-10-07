using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadMainScene(int sceneNo)
	{
		PlayerPrefs.SetInt("reset", 0);
		PlayerPrefs.SetFloat ("PlayerX", 0.0f);
		PlayerPrefs.SetFloat ("PlayerY", 0.0f);
		PlayerPrefs.SetFloat ("PlayerZ", 0.0f);
		SceneManager.LoadScene(sceneNo);

	}
}
