using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CharController : MonoBehaviour {
	PlayerIndex index;
	public bool dead = false;

    public enum PlayerNum { player1, player2, player3, player4}
    float move; //what direction char is facing
    public float movespeed = 10f; //movespeed
    public float jumpForce = 10f;
    public float airSpeed = 2f;
    bool facingRight = true; //direction char is facing
    bool jumped = false; //turns to true when button is pressed
    bool grounded = false; //is Player in air
    public bool change = false;
    public bool checkIn = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public Transform SpawnLocation;
    public PlayerNum playerNumber;
    Entity.Element type;
    Entity.Element changeType;
    public AudioClip[] fishMoveSFX;
    public AudioClip[] catMoveSFX;
    public AudioClip[] birdMoveSFX;
    public AudioClip fishJumpSFX;
    public AudioClip catJumpSFX;
    public AudioClip birdJumpSFX;
    public AudioClip fishLandSFX;
    public AudioClip catLandSFX;
    public AudioClip birdLandSFX;
    public AudioClip rustleSFX;
    public Light myLight;

    Rigidbody2D rb;
    SpriteRenderer spr;
    Animator anim;
    Entity ent;
    AudioSource aud;

	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<CharController> ().playerNumber == CharController.PlayerNum.player1) {
			index = PlayerIndex.One;
		} else {
			index = PlayerIndex.Two;
		}
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        type = gameObject.GetComponent<Entity>().type;
        ent = gameObject.GetComponent<Entity>();
        aud = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			//PLAYER ONE INPUTS
			if (playerNumber == PlayerNum.player1) {
				if ((Input.GetButtonDown ("Jump") || Input.GetAxis ("Vertical") == 1) && grounded && !GameManager.gameStart) {
					//Debug.Log("JUMP");
					jumped = true;
				}
				//Debug.Log(Input.GetAxisRaw("DpadX"));
				if (GameObject.FindGameObjectWithTag ("StateManager").GetComponent<StateManager> ().canChange) {
					if (Input.GetAxisRaw ("DpadX") < 0) {
						changeType = Entity.Element.bass;
						checkIn = true;
						StartCoroutine (Vibrate (.1f, .2f));
					}
					if (Input.GetAxisRaw ("DpadX") > 0) {
						changeType = Entity.Element.horn;
						checkIn = true;
						StartCoroutine (Vibrate (.1f, .2f));
					}
					if (Input.GetAxisRaw ("DpadY") > 0) {
						changeType = Entity.Element.guitar;
						checkIn = true;
						StartCoroutine (Vibrate (.1f, .2f));
					}


				}
			}

			//PLAYER TWO INPUTS
			if (playerNumber == PlayerNum.player2) {
				if ((Input.GetButtonDown ("Jump2") || Input.GetAxis ("Vertical2") == 1) && grounded&& !GameManager.gameStart) {
					//Debug.Log("JUMP");
					jumped = true;
				}
				//Debug.Log(Input.GetAxisRaw("DpadX"));
				if (GameObject.FindGameObjectWithTag ("StateManager").GetComponent<StateManager> ().canChange) {
					if (Input.GetAxisRaw ("DpadX2") < 0) {
						changeType = Entity.Element.bass;
						checkIn = true;
						StartCoroutine (Vibrate (.1f, .2f));
					}
					if (Input.GetAxisRaw ("DpadX2") > 0) {
						changeType = Entity.Element.horn;
						checkIn = true;
						StartCoroutine (Vibrate (.1f, .2f));
					}
					if (Input.GetAxisRaw ("DpadY2") > 0) {
						changeType = Entity.Element.guitar;
						checkIn = true;
						StartCoroutine (Vibrate (.1f, .2f));
					}


				}
			}

			//TRANSFORMATION
			if (change) {
				transformation ();
			}

		}

    }

    private void FixedUpdate()
    {
		if (!dead&& !GameManager.gameStart) {
			//Debug.Log(grounded);
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			if (playerNumber == PlayerNum.player1) {
				move = Input.GetAxisRaw ("Horizontal");
			} else if (playerNumber == PlayerNum.player2) {
				move = Input.GetAxisRaw ("Horizontal2");
			}
			//Debug.Log("MOVE: " + move);
			//Moves Player left and right
			if (grounded) {
				anim.SetBool ("Grounded", true);
				rb.velocity = new Vector2 (move * movespeed, rb.velocity.y);
			} else {
				anim.SetBool ("Grounded", false);
				rb.velocity = new Vector2 (move * movespeed / airSpeed, rb.velocity.y);
			}
			anim.SetFloat ("Speed", Mathf.Abs (move));

			//Jump
			if (jumped) {
           
				rb.velocity = new Vector2 (0.0f, jumpForce);
				jumped = false;
			}

			//flip if moving other way
			if (move < 0 && facingRight) {
				//spr.flipX = true;
				facingRight = false;
				transform.localScale = new Vector3 (
					transform.localScale.x * -1,
					transform.localScale.y,
					transform.localScale.z);
			}
			if (move > 0 && !facingRight) {
				facingRight = true;
				transform.localScale = new Vector3 (
					transform.localScale.x * -1,
					transform.localScale.y,
					transform.localScale.z);
			}
		}
	}
	IEnumerator Vibrate(float secs, float amount){
		GamePad.SetVibration (index, 0f, 0f);
		GamePad.SetVibration (index, amount, amount);
		yield return new WaitForSeconds (secs);
		GamePad.SetVibration (index, 0f, 0f);
	}

    public void changeRumble()
    {
        Debug.Log("NO RUMBLE");
        anim.SetBool("Rumbling", false);
    }

    public void transformation()
    {
        if (type != changeType)
        {
            type = changeType;
            gameObject.GetComponent<Entity>().changeType(type);
            if (type == Entity.Element.bass)
            {
                anim.SetInteger("Form", -1);
            }
            if (type == Entity.Element.guitar)
            {
                anim.SetInteger("Form", 0);
            }
            if (type == Entity.Element.horn)
            {
                anim.SetInteger("Form", 1);
            }
            GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>().checkAmmo();
        }
        anim.SetBool("Rumbling", true);
        change = false;
        checkIn = false;
    }

    public void playFishMovement()
    {
        int randomSound = Random.Range(0, fishMoveSFX.Length);
        aud.clip = fishMoveSFX[randomSound];
        aud.Play();
    }

    public void playFishJump()
    {
        aud.clip = fishJumpSFX;
        aud.Play();
    }

    public void playFishLand()
    {
        aud.clip = fishLandSFX;
        aud.Play();
    }

    public void playCatMovement()
    {
        int randomSound = Random.Range(0, catMoveSFX.Length);
        aud.clip = catMoveSFX[randomSound];
        aud.Play();
    }

    public void playCatJump()
    {
        aud.clip = catJumpSFX;
        aud.Play();
    }

    public void playCatLand()
    {
        aud.clip = catLandSFX;
        aud.Play();
    }
    public void playBirdMovement()
    {
        int randomSound = Random.Range(0, catMoveSFX.Length);
        aud.clip = catMoveSFX[randomSound];
        aud.Play();
    }

    public void playBirdJump()
    {
        aud.clip = catJumpSFX;
        aud.Play();
    }

    public void playBirdLand()
    {
        aud.clip = catLandSFX;
        aud.Play();
    }

    public void playDashSound()
    {

    }

    public void playRustle()
    {
        aud.clip = rustleSFX;
        aud.Play();
    }

    public void glow()
    {
        myLight.intensity += 10;
        if (type == Entity.Element.bass)
        {
            myLight.color = new Color(0 / 255f, 109 / 255f, 255/ 255f);
        }
        if (type == Entity.Element.guitar)
        {
            myLight.color = new Color(255 / 255f, 91 / 255f, 76/ 255f);
        }
        if (type == Entity.Element.horn)
        {
            myLight.color = new Color(76 / 255f, 255 / 255f, 91 / 255f);
        }
       
    }

    public void deGlow()
    {
        myLight.intensity = 4f;
        myLight.color = Color.white;
    }

}
