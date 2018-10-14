using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using XInputDotNetPure;

public class PlayerChar : MonoBehaviour {
	PlayerIndex index;
	public bool scored = false;
	public Text winText;

	public Vector2 spawn;
   
    public int maxHealth = 10;
	public int health;
	public Transform healthBar;

	public int damageFromSame = 2;
	public int damageFromWeakness = 3;
	public int damageFromStrength = 1;

    public AudioClip weakHit;
    public AudioClip normalHit;
    public AudioClip strongHit;
    public AudioClip deathSound;

    public AudioSource playerAud;
    public AudioSource deathAud;

    //public float blasterSpeed = 50f;

    // Use this for initialization
	void Start () {
		if (gameObject.GetComponent<CharController> ().playerNumber == CharController.PlayerNum.player1) {
			index = PlayerIndex.One;
		} else {
			index = PlayerIndex.Two;
		}
		//StartCoroutine(Vibrate (0.1f, 0f));
		winText.gameObject.SetActive(false);
		health = maxHealth;
		spawn = new Vector2 (transform.position.x, transform.position.y);
        playerAud = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//GUI.Label(new Rect(healthBar.transform.x, healthBar.transform.y, healthBar.localScale.x * ((float)health/(float)(maxHealth)), healthBar.localScale.y), "text");
		healthBar.transform.localScale = new Vector2(((float)health/(float)(maxHealth)),healthBar.transform.localScale.y);
		Debug.Log (healthBar.transform.localScale);
		if (health <= 0) {
			StartCoroutine (Death ());
		}
	}

	public void DamagePlayer(GameObject shooter, Entity.Element otherType){

		Entity.Element myType = gameObject.GetComponent<Entity>().type;
		Debug.Log ("collision with " + otherType);

		if (otherType == myType) { //if same type
			StartCoroutine(Vibrate(.3f, 0.5f));
			Debug.Log(myType + " hit by its own type");
			health -= damageFromSame;
            playerAud.clip = normalHit;
            playerAud.Play();
            CameraShaker.Instance.ShakeOnce(2f, 4f, .1f, 1.5f);
        }

		//RPS:
		//BASS beats GUITAR beats HORN beats...
		if (myType == Entity.Element.guitar) { //if col is guitar
			if (otherType == Entity.Element.bass) {	
				Debug.Log ("guitar weak to bass");
				StartCoroutine(Vibrate(.3f, 5f));
				health -= damageFromWeakness;
                playerAud.clip = strongHit;
                playerAud.Play();
                CameraShaker.Instance.ShakeOnce(3f, 7f, .1f, 2f);
            } else if (otherType == Entity.Element.horn) {
				Debug.Log ("guitar strong against horn");
				StartCoroutine(Vibrate(.2f, 0.2f));
				health -= damageFromStrength;
                playerAud.clip = weakHit;
                playerAud.Play();
                CameraShaker.Instance.ShakeOnce(1f, 1f, .1f, 1f);
            }
		}
		else if (myType == Entity.Element.bass) { //if col is bass
			if (otherType == Entity.Element.horn) {	
				Debug.Log ("bass weak to horn");
				StartCoroutine(Vibrate(.3f, 5f));
				health -= damageFromWeakness;
                playerAud.clip = strongHit;
                playerAud.Play();
                CameraShaker.Instance.ShakeOnce(3f, 7f, .1f, 2f);
            } else if (otherType == Entity.Element.guitar) {
				Debug.Log ("bass strong against guitar");
				StartCoroutine(Vibrate(.2f, 0.2f));
				health -= damageFromStrength;
                playerAud.clip = weakHit;
                playerAud.Play();
                CameraShaker.Instance.ShakeOnce(1f, 1f, .1f, 1f);
            }
		}
		else if (myType == Entity.Element.horn) { //if col is horn
			if (otherType == Entity.Element.guitar){	
				Debug.Log ("horn weak to guitar");
				StartCoroutine(Vibrate(.3f, 5f));
				health -= damageFromWeakness;
                playerAud.clip = strongHit;
                playerAud.Play();
                CameraShaker.Instance.ShakeOnce(3f, 7f, .1f, 2f);
            } else if (otherType == Entity.Element.bass) {
				Debug.Log ("horn strong against bass");
				StartCoroutine(Vibrate(.2f, 0.2f));
				health -= damageFromStrength;
                playerAud.clip = weakHit;
                playerAud.Play();
                CameraShaker.Instance.ShakeOnce(1f, 1f, .1f, 1f);
            }
		}
    }

	public int GetHealth(){return health;}

	IEnumerator Vibrate(float secs, float amount){
		GamePad.SetVibration (index, 0f, 0f);
		GamePad.SetVibration (index, amount, amount);
		yield return new WaitForSeconds (secs);
		GamePad.SetVibration (index, 0f, 0f);
	}
	IEnumerator Death(){
        GameManager.roundOver = true;
		winText.gameObject.SetActive (true);
		health = 0;
		if (!scored) {
			deathAud.clip = deathSound;
			StartCoroutine(Vibrate(2f, 5f));
			if(!deathAud.isPlaying)deathAud.Play();
			CameraShaker.Instance.ShakeOnce(5f, 9f, 1f, 3f);
			if (gameObject.GetComponent<CharController> ().playerNumber.Equals (CharController.PlayerNum.player1)) {
				GameManager.score_two++;
			} else {
				GameManager.score_one++;
			}
			scored = true;
		}
		gameObject.GetComponent<CharController> ().dead = true;
		yield return new WaitForSeconds (3f);
		if (GameManager.score_one >= 5 || GameManager.score_two >= 5)
			GameManager.Win ();
		else
			GameManager.NextScene (); 
	}
}
