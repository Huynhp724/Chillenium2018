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
    public Sprite bird, cat, fish, mystery, mystery2;

	public GameObject arrow;
	Image arrowImg;

	public GameObject pause;

    public float countdownTime; //How long between each "phase"
    public float checkInTime; //When player can check in
    float counter;
    public bool canChange;
    public AudioClip[] countdownClips;
    public AudioClip beginClip;
    public AudioSource aud;
    public int clipTracker;
    public GameObject[] players;

	public Transform[] boxes;
	public Transform[] boxes2;

    public Sprite fishNotes;
    public Sprite catNotes;
    public Sprite birdNotes;

    public bool begin = true;
    public Animator anim1;
    public Animator anim2;

    // Use this for initialization
    void Start() {
		Time.timeScale = 1;
        Physics2D.gravity = new Vector3(0.0f, gravityForce, 0.0f);
		counter = checkInTime + 1f;
        // players = GameObject.FindGameObjectsWithTag("Player");
        checkInText.text = "";
        checkInText2.text = "";
        clipTracker = countdownClips.Length;
		checkAmmo();
		StartCoroutine (Waiting ());
		arrowImg = arrow.gameObject.GetComponentInChildren<Image> ();
    }
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.roundOver) {
			//counter
			int counterNum = (int)counter;
			timerText.text = counterNum.ToString ();
			counter -= Time.deltaTime;

            //Pausing
			if(Input.GetButtonDown("Submit")){
				if(Time.timeScale == 1){
					Time.timeScale = 0;
					pause.SetActive(true);
				}else if(Time.timeScale == 0){
					Time.timeScale = 1;
					pause.SetActive(false);
				}
			}
			//If counter gets to check in time, allows player to lock in a choice
			if (counterNum < countdownClips.Length && clipTracker != counterNum) {
				canChange = true;
                if (!begin)
                {
                    CameraShaker.Instance.ShakeOnce(4f - counterNum, 7f - counterNum, .1f, 1.5f);
                }
                else
                {
                    CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 4f);
                }
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<CharController>().glow();
                }
				timerText.color = Color.yellow;
                if (begin && counterNum == 0)
                {
                    aud.clip = beginClip;
                }
                else
                {
                    aud.clip = countdownClips[counterNum];
                }
				aud.Play ();
				clipTracker--;
			}

			switch (players [0].GetComponent<Entity> ().type) {
			case Entity.Element.bass:
				portrait.sprite = fish;
				if (players [1].GetComponent<Entity> ().type == Entity.Element.guitar) {
					arrow.SetActive (true);
					arrowImg.transform.localScale = new Vector2 (1f, 1f);
				} else if (players [1].GetComponent<Entity> ().type == Entity.Element.bass) {
					arrow.SetActive (false);
				} else if (players [1].GetComponent<Entity> ().type == Entity.Element.horn) {
					arrow.SetActive (true);
					arrowImg.transform.localScale = new Vector2 (-1f, 1f);
				}
				break;
			case Entity.Element.guitar:
				portrait.sprite = cat;
				if (players [1].GetComponent<Entity> ().type == Entity.Element.guitar) {
					arrow.SetActive (false);
				} else if (players [1].GetComponent<Entity> ().type == Entity.Element.bass) {
					arrow.SetActive (true);
					arrowImg.transform.localScale = new Vector2 (-1f, 1f);
				} else if (players [1].GetComponent<Entity> ().type == Entity.Element.horn) {
					arrow.SetActive (true);
					arrowImg.transform.localScale = new Vector2 (1f, 1f);
				}
				break;
			case Entity.Element.horn:
				portrait.sprite = bird;
				if (players [1].GetComponent<Entity> ().type == Entity.Element.guitar) {
					arrow.SetActive (true);
					arrowImg.transform.localScale = new Vector2 (-1f, 1f);
				} else if (players [1].GetComponent<Entity> ().type == Entity.Element.bass) {
					arrow.SetActive (true);
					arrowImg.transform.localScale = new Vector2 (1f, 1f);
				} else if (players [1].GetComponent<Entity> ().type == Entity.Element.horn) {
					arrow.SetActive (false);
				}
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
            if (begin)
            {
                portrait.sprite = mystery;
                portrait2.sprite = mystery2;
                portrait2.transform.localScale = new Vector2(1f, 1f);
               
            }
            else
            {
            }
		}

		if (counter <= 4 && counter > 3 || (counter <= 2 && counter > 0)) {
			checkInText.text = "TRANSFORM NOW!";
			checkInText2.text = "TRANSFORM NOW!";
		} else if (counter <= 3 && counter > 2) {
			checkInText.text = "";
			checkInText2.text = "";
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
                else
                {
                    if (begin)
                    {
                        
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                players[i].GetComponent<CharController>().forcedTransformation(Entity.Element.bass);
                                break;
                            case 1:
                                players[i].GetComponent<CharController>().forcedTransformation(Entity.Element.guitar);
                                break;
                            case 2:
                                players[i].GetComponent<CharController>().forcedTransformation(Entity.Element.horn);
                                break;
                            default:
                                break;
                        }

                    }
                }
                players[i].GetComponent<CharController>().deGlow();
            }
            counter = countdownTime;
            timerText.color = Color.white;
            canChange = false;
            checkInText.text = "";
            checkInText2.text = "";
            clipTracker = countdownClips.Length;
            if (begin)
            {
                begin = false;
                anim1.SetBool("Begin", false);
                anim2.SetBool("Begin", false);
                portrait2.transform.localScale = new Vector2(-1f, 1f);
            }
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
