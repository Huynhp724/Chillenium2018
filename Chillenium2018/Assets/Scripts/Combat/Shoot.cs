﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {
	public GameObject projectile;
    public Transform spawnLocation;
    public AudioClip[] attackSFX;
    AudioSource[] attackSources;
    int audioCount = 0;

	public int max_projectiles = 5;
	public int num_projectiles = 0;
	public int projectiles_remaining = 5;

	float timer;
	public float reload_time = 0.25f;
	public float spam_time = 0.75f;
    public float blasterSpeed = 20f;

    private void Awake()
    {
        attackSources = new AudioSource[attackSFX.Length];
       for(int i = 0; i < attackSFX.Length; i++)
        {
            attackSources[i] = gameObject.AddComponent<AudioSource>();
            attackSources[i].volume = 0.05f;
        }
    }
    // Use this for initialization
    void Start () {
		timer = reload_time;
	}
	
	// Update is called once per frame
	void Update () {

		//TEMP DISPLAY
		string t = "";
		projectiles_remaining = max_projectiles - num_projectiles;
		switch (projectiles_remaining) {
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

		//TIMER
		if(timer > 0f)
			timer -= Time.deltaTime; // inc timer
		else if (timer <= 0f && num_projectiles > 0) { //reload
			num_projectiles--;
			timer = reload_time;
		}

		//FIRE
		if (num_projectiles < max_projectiles) { //able to fire
            if (gameObject.GetComponent<CharController>().playerNumber == CharController.PlayerNum.player1)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    FireProjectile();
                    num_projectiles++;
                    timer = spam_time;

                    //Plays projectile sound
                    int clipNum = Random.Range(0, attackSFX.Length);
                    attackSources[audioCount].clip = attackSFX[clipNum];
                    attackSources[audioCount].Play(0);
                    audioCount++;
                    if (audioCount >= attackSFX.Length)
                    {
                        audioCount = 0;
                    }
                }
            }
            else if (gameObject.GetComponent<CharController>().playerNumber == CharController.PlayerNum.player2)
            {
                if (Input.GetButtonDown("Fire1_2"))
                {
                    FireProjectile();
                    num_projectiles++;
                    timer = spam_time;

                    //Plays projectile sound
                    int clipNum = Random.Range(0, attackSFX.Length);
                    attackSources[audioCount].clip = attackSFX[clipNum];
                    attackSources[audioCount].Play(0);
                    audioCount++;
                    if (audioCount >= attackSFX.Length)
                    {
                        audioCount = 0;
                    }
                }
            }
        } else { //cannot fire; spam

		}

	}
	void FireProjectile(){
		GameObject projectileClone = (GameObject)Instantiate(projectile, spawnLocation.position, spawnLocation.rotation);
        projectileClone.GetComponent<Entity>().type = gameObject.GetComponent<Entity>().type;
        projectileClone.GetComponent<ProjectileHandler>().speed = gameObject.transform.localScale.x * blasterSpeed;
        projectileClone.GetComponent<ProjectileHandler>().source = gameObject;
    }
}
