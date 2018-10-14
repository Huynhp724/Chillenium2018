using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
	public static GameManager instance = null;

	public static bool roundOver;
	public static bool gameStart;

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
		roundOver = false;
		gameStart = true;
	}
	public static void ResetScene(){
		roundOver = false;
		gameStart = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public static void NextScene(){
		roundOver = false;
		gameStart = true;
		//switch between two scenes
		int stage = Random.Range(2, 5);
		SceneManager.LoadScene (stage);
	}
	public static void Win(){
		SceneManager.LoadScene (5);
	}
	public static void MainMenu(){
		SceneManager.LoadScene (0);
	}
}