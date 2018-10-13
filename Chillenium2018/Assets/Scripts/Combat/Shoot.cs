using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {
	public GameObject projectile;
    public Transform spawnLocation;
	public Text projectiles_remaining;

	public int max_projectiles = 5;
	int num_projectiles = 0;

	float timer;
	public float reload_time = 0.25f;
	public float spam_time = 0.75f;

	// Use this for initialization
	void Start () {
		timer = reload_time;
	}
	
	// Update is called once per frame
	void Update () {

		//TEMP DISPLAY
		string t = "";
		switch (max_projectiles - num_projectiles) {
		case 0:
			t = "- - - - -";
			break;
		case 1:
			t = "+ - - - -";
			break;
		case 2:
			t = "+ + - - -";
			break;
		case 3:
			t = "+ + + - -";
			break;
		case 4:
			t = "+ + + + -";
			break;
		case 5:
			t = "+ + + + +";
			break;
		}
		projectiles_remaining.text = "Timer: " + timer + "\nActive: " + num_projectiles + "\nRemaining: " + (max_projectiles - num_projectiles) + "\n" + t;

		//TIMER
		if(timer > 0f)
			timer -= Time.deltaTime; // inc timer
		else if (timer <= 0f && num_projectiles > 0) { //reload
			num_projectiles--;
			timer = reload_time;
		}

		//FIRE
		if (num_projectiles < max_projectiles) { //able to fire
			if (Input.GetButtonDown ("Fire1")) {
				FireProjectile ();
				num_projectiles++;
				timer = spam_time;
			}
		} else { //cannot fire; spam

		}

	}
	void FireProjectile(){
		GameObject projectileClone = (GameObject)Instantiate(projectile, spawnLocation.position, spawnLocation.rotation);
        projectileClone.GetComponent<ProjectileHandler>().type = gameObject.GetComponent<PlayerChar>().type;
        projectileClone.GetComponent<ProjectileHandler>().speed = gameObject.transform.localScale.x * gameObject.GetComponent<PlayerChar>().blasterSpeed;
        projectileClone.GetComponent<ProjectileHandler>().source = gameObject;
    }
}
