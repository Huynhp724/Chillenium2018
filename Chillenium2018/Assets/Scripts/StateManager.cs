using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class StateManager : MonoBehaviour {
    public float gravityForce;

    public Text timerText;

    public Text checkInText;
    public Text checkInText2;

	public Text scoreText;
	public Text scoreText2;

	public Image portrait, portrait2;
	public Sprite bird, cat, fish;

    public float countdownTime; //How long between each "phase"
    public float checkInTime; //When player can check in
    float counter;
    public bool canChange;
    public AudioClip[] countdownClips;
    public AudioSource aud;
    public int clipTracker;
    public GameObject[] players;

	public Transform[] boxes;
	public Transform[] boxes2;

    public Sprite fishNotes;
    public Sprite catNotes;
    public Sprite birdNotes;
    

    // Use this for initialization
    void Start() {
        Physics2D.gravity = new Vector3(0.0f, gravityForce, 0.0f);
		counter = checkInTime + 1f;
        // players = GameObject.FindGameObjectsWithTag("Player");
        checkInText.text = "Standby...";
        checkInText2.text = "Standby...";
        clipTracker = countdownClips.Length;
		checkAmmo();
		StartCoroutine (Waiting ());
    }
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.roundOver) {
			//counter
			int counterNum = (int)counter;
			timerText.text = counterNum.ToString ();
			counter -= Time.deltaTime;

			//If counter gets to check in time, allows player to lock in a choice
			if (counterNum < countdownClips.Length && clipTracker != counterNum) {
				canChange = true;
                CameraShaker.Instance.ShakeOnce(4f - counterNum, 7f - counterNum, .1f, 1.5f);
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<CharController>().glow();
                }
				timerText.color = Color.yellow;
				aud.clip = countdownClips [counterNum];
				aud.Play ();
				clipTracker--;
			}

			switch (players [0].GetComponent<Entity> ().type) {
			case Entity.Element.bass:
				portrait.sprite = fish;
				break;
			case Entity.Element.guitar:
				portrait.sprite = cat;
				break;
			case Entity.Element.horn:
				portrait.sprite = bird;
				break;
			}
			switch (players [1].GetComponent<Entity> ().type) {
			case Entity.Element.bass:
				portrait2.sprite = fish;
				break;
			case Entity.Element.guitar:
				portrait2.sprite = cat;
				break;
			case Entity.Element.horn:
				portrait2.sprite = bird;
				break;
			}
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
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<CharController>().checkIn)
                {
                    players[i].GetComponent<CharController>().change = true;

                }
                players[i].GetComponent<CharController>().deGlow();
            }
            counter = countdownTime;
            timerText.color = Color.white;
            canChange = false;
            checkInText.text = "Standby...";
            checkInText2.text = "Standby...";
            clipTracker = countdownClips.Length;
        }

        //score
        scoreText.text = "" + GameManager.score_one;
        scoreText2.text = "" + GameManager.score_two;

        //health
        //healthText.text = "P1 Health: " + players[0].GetComponent<PlayerChar>().GetHealth();
        //healthText2.text = "P2 Health: " + players[1].GetComponent<PlayerChar>().GetHealth();

        //ammo
        for (int index = 1; index <= 5; index++) {
            Debug.Log("projectiles remaining" + players[0].GetComponent<Shoot>().projectiles_remaining);
            if (players[0].GetComponent<Shoot>().projectiles_remaining < index) {
                boxes[index - 1].gameObject.SetActive(false);
            } else {
                boxes[index - 1].gameObject.SetActive(true);
            }
        }
        for (int index = 1; index <= 5; index++) {
            if (players[1].GetComponent<Shoot>().projectiles_remaining < index) {
                boxes2[index - 1].gameObject.SetActive(false);
            } else {
                boxes2[index - 1].gameObject.SetActive(true);
            }
        }
    }

	IEnumerator Waiting(){
		yield return new WaitForSeconds (4f);
		GameManager.gameStart = false;
	}

    public void checkAmmo()
    {
        if (players[0].GetComponent<Entity>().type == Entity.Element.bass)
        {
            setAmmo(fishNotes, 1);
        }
        else if (players[0].GetComponent<Entity>().type == Entity.Element.guitar)
        {

            setAmmo(catNotes, 1);
        }
        else if (players[0].GetComponent<Entity>().type == Entity.Element.horn)
        {
            setAmmo(birdNotes, 1);
        }

        if (players[1].GetComponent<Entity>().type == Entity.Element.bass)
        {
            setAmmo(fishNotes, 2);
        }
        else if (players[1].GetComponent<Entity>().type == Entity.Element.guitar)
        {

            setAmmo(catNotes, 2);
        }
        else if (players[1].GetComponent<Entity>().type == Entity.Element.horn)
        {
            setAmmo(birdNotes, 2);
        }
    }
    public void setAmmo(Sprite notes, int player)
    {
        if (player == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                boxes[i].GetComponent<Image>().sprite = notes;
            }
        }
        else if (player == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                boxes2[i].GetComponent<Image>().sprite = notes;
            }
        }
    }
}
