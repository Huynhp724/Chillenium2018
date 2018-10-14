using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {
	public Text whoWon;
	public Text finalScore;
    public AudioSource aud;
    public AudioClip player1Win;
    public AudioClip player2Win;
	// Use this for initialization
	void Start () {
		whoWon.text = "PLAYER " + (GameManager.score_one > GameManager.score_two ? "1" : "2") + " WON";
		finalScore.text = "Score:\n" + GameManager.score_one + " to " + GameManager.score_two;
        if(GameManager.score_one > GameManager.score_two)
        {
            aud.clip = player1Win;
            aud.Play();
        }
        if (GameManager.score_one < GameManager.score_two)
        {
            aud.clip = player2Win;
            aud.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			GameManager.MainMenu ();
		}
	}
}
