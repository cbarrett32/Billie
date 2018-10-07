using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoftReset_2 : MonoBehaviour {
	GameObject Billie;
	int reset = 0;

	// Use this for initialization
	void Start () {
		Billie = GameObject.Find ("Billie");
		reset = PlayerPrefs.GetInt ("reset");
		if (reset == 1) {
			float newX = PlayerPrefs.GetFloat("PlayerX_2");
			float newY = PlayerPrefs.GetFloat("PlayerY_2"); 
			float newZ = PlayerPrefs.GetFloat("PlayerZ_2");
			Billie.transform.position = new Vector3 (newX, newY, newZ);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadSceneOnClick(int sceneNo)
	{

		PlayerPrefs.SetInt("reset", 1);
		SceneManager.LoadScene(sceneNo);

	}

	public void HardReset(int sceneNo)
	{
		PlayerPrefs.SetInt("flag", 0);

		PlayerPrefs.SetInt("reset", 0);
		PlayerPrefs.SetFloat ("PlayerX", 0.0f);
		PlayerPrefs.SetFloat ("PlayerY", 0.0f);
		PlayerPrefs.SetFloat ("PlayerZ", 0.0f);
		SceneManager.LoadScene(sceneNo);

	}
}
