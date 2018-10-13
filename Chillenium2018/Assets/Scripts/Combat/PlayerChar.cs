using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChar : MonoBehaviour {
	public int score = 0;

	public Vector2 spawn;
   
    public int maxHealth = 10;
	public int health;

	public int damageFromSame = 2;
	public int damageFromWeakness = 3;
	public int damageFromStrength = 1;

	public Text debugTextHealth;
	public Text debugTextScore;

    public float blasterSpeed = 50f;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		spawn = new Vector2 (transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
			Death ();
		debugTextHealth.text = "HEALTH: " + health;
		debugTextScore.text = "SCORE: " + score;
	}

	public void DamagePlayer(GameObject shooter, Entity.Element otherType){

		Entity.Element myType = gameObject.GetComponent<Entity>().type;
		Debug.Log ("collision with " + otherType);

		if (otherType == myType) { //if same type
			Debug.Log(myType + " hit by its own type");
			health -= damageFromSame;
		}

		//RPS:
		//BASS beats GUITAR beats HORN beats...
		if (myType == Entity.Element.guitar) { //if col is guitar
			if (otherType == Entity.Element.bass) {	
				Debug.Log ("guitar weak to bass");
				health -= damageFromWeakness;
			} else if (otherType == Entity.Element.horn) {
				Debug.Log ("guitar strong against horn");
				health -= damageFromStrength;
			}
		}
		else if (myType == Entity.Element.bass) { //if col is bass
			if (otherType == Entity.Element.horn) {	
				Debug.Log ("bass weak to horn");
				health -= damageFromWeakness;
			} else if (otherType == Entity.Element.guitar) {
				Debug.Log ("bass strong against guitar");
				health -= damageFromStrength;
			}
		}
		else if (myType == Entity.Element.horn) { //if col is horn
			if (otherType == Entity.Element.guitar){	
				Debug.Log ("horn weak to guitar");
				health -= damageFromWeakness;
			} else if (otherType == Entity.Element.bass) {
				Debug.Log ("horn strong against bass");
				health -= damageFromStrength;
			}
		}
        if (health <= 0)
        {
            Death();
            shooter.GetComponent<PlayerChar>().score++;
        }
    }

	void Death(){
		
		transform.SetPositionAndRotation (new Vector3 (spawn.x, spawn.y, transform.position.z), transform.rotation);
		health = maxHealth;
	}
}
