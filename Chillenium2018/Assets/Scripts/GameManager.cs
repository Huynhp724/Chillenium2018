using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
	public static GameManager instance = null;

	public static bool roundOver = false;

	//global variables
	public static int score_one = 0;
	public static int score_two = 0;
	// Use this for initialization
	void Awake () {
		if (!instance) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
	public static void ResetScene(){
		roundOver = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	
	// Update is called once per frame
	void Update () {
	}
}
