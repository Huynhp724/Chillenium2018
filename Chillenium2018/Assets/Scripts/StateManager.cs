using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {

    public float gravityForce;
    public Text timerText;
    public Text checkInText;
    public Text checkInText2;
    public float countdownTime; //How long between each "phase"
    public float checkInTime; //When player can check in
    float counter;
    public bool canChange;
    GameObject[] players;
    public AudioClip[] countdownClips;
    public AudioSource aud;
    public int clipTracker;
    // Use this for initialization
    void Start () {
        Physics2D.gravity = new Vector3(0.0f, gravityForce, 0.0f);
        counter = countdownTime;
        players = GameObject.FindGameObjectsWithTag("Player");
        checkInText.text = "Standby...";
        checkInText2.text = "Standby...";
        clipTracker = countdownClips.Length;
    }
	
	// Update is called once per frame
	void Update () {
        int counterNum = (int)counter;
        timerText.text = counterNum.ToString();
        counter -= Time.deltaTime;
        //If counter gets to check in time, allows player to lock in a choice
        if(counterNum < countdownClips.Length && clipTracker != counterNum)
        {
            canChange = true;
            timerText.color = Color.yellow;
            aud.clip = countdownClips[counterNum];
            aud.Play();
            clipTracker--;
        }
       
        //If player locks in, update their lock in message
        if (players[0].GetComponent<CharController>().checkIn)
        {
            checkInText.text = "Locked In!";
        }
        if (players.Length > 1 && players[1].GetComponent<CharController>().checkIn)
        {
            checkInText2.text = "Locked In!";
        }
        //Once time reaches 0, resets and transforms players who locked in
        if (counter < 0)
        {
            
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<CharController>().checkIn)
                {
                    players[i].GetComponent<CharController>().change = true;
                   
                }
            }
            counter = countdownTime;
            timerText.color = Color.white;
            canChange = false;
            checkInText.text = "Standby...";
            checkInText2.text = "Standby...";
            clipTracker = countdownClips.Length;

        }
	}
}
