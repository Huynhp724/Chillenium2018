using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {

    public float gravityForce;
    public Text timerText;
    public Text checkInText;
    public float countdownTime; //How long between each "phase"
    public float checkInTime; //When player can check in
    float counter;
    public bool canChange;
    public bool checkIn;

	// Use this for initialization
	void Start () {
        Physics2D.gravity = new Vector3(0.0f, gravityForce, 0.0f);
        counter = countdownTime;
	}
	
	// Update is called once per frame
	void Update () {
        int counterNum = (int)counter;
        timerText.text = counterNum.ToString();
        counter -= Time.deltaTime;
        //If counter gets to check in time, allows player to lock in a choice
        if(counterNum <= checkInTime)
        {
            canChange = true;
            timerText.color = Color.yellow;
        }
        //Once time reaches 0, resets and transforms players who locked in
        if(counter < 0)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < players.Length; i++)
            {
                if(checkIn)
                    players[i].GetComponent<CharController>().change = true;
            }
            counter = countdownTime;
            timerText.color = Color.white;
            canChange = false;
            checkIn = false;
        }
        checkInText.text = checkIn.ToString();
	}
}
