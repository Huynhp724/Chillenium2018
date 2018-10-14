using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {
	public Text whoWon;
	public Text finalScore;

	// Use this for initialization
	void Start () {
		whoWon.text = "PLAYER " + (GameManager.score_one > GameManager.score_two ? "1" : "2") + " WON";
		finalScore.text = "Score:\n" + GameManager.score_one + " to " + GameManager.score_two;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
