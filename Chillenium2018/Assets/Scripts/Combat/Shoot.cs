﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {
	public GameObject catNote;
    public GameObject fishNote;
    public GameObject birdNote;

    public Transform spawnLocation;
    public int attackNum;
    AudioSource[] attackSources;
    public AudioClip[] fishSFX;
    public AudioClip[] catSFX;
    public AudioClip[] birdSFX;
    int audioCount = 0;

	public int max_projectiles = 5;
	public int num_projectiles = 0;
	public int projectiles_remaining = 5;

	float timer;
	public float reload_time = 0.25f; //how fast you can spam
	public float spam_time = 0.75f; //how fast bullets come back
    public float blasterSpeed = 20f;

    public Sprite fishNotes;
    public Sprite catNotes;
    public Sprite birdNotes;

    Animator anim;

    private void Awake()
    {
        attackSources = new AudioSource[attackNum];
       for(int i = 0; i < attackNum; i++)
        {
            attackSources[i] = gameObject.AddComponent<AudioSource>();
            attackSources[i].volume = 0.05f;
        }
    }
    // Use this for initialization
	void Start () {
		timer = reload_time;
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//TIMER
		if(timer > 0f)
			timer -= Time.deltaTime; // inc timer
		else if (timer <= 0f && num_projectiles > 0) { //reload
			num_projectiles--;
			timer = reload_time;
		}
		if (!gameObject.GetComponent<CharController> ().dead && !GameManager.gameStart) {
			//FIRE
			projectiles_remaining = max_projectiles - num_projectiles;
			if (num_projectiles < max_projectiles) { //able to fire
				if (gameObject.GetComponent<CharController> ().playerNumber == CharController.PlayerNum.player1) {
					if (Input.GetButtonDown ("Fire1")) {

                        anim.SetBool("Attacking", true);
						FireProjectile ();
						num_projectiles++;
						timer = spam_time;

						//Plays projectile sound
						int clipNum = Random.Range (0, attackNum);
						if (gameObject.GetComponent<Entity> ().type == Entity.Element.bass) {
							attackSources [audioCount].clip = fishSFX [clipNum];
						}
						if (gameObject.GetComponent<Entity> ().type == Entity.Element.guitar) {
							attackSources [audioCount].clip = catSFX [clipNum];
						}
						if (gameObject.GetComponent<Entity> ().type == Entity.Element.horn) {
							attackSources [audioCount].clip = birdSFX [clipNum];
						}
						attackSources [audioCount].Play (0);
						audioCount++;
						if (audioCount >= attackNum) {
							audioCount = 0;
						}
					}
				} else if (gameObject.GetComponent<CharController> ().playerNumber == CharController.PlayerNum.player2) {
					if (Input.GetButtonDown ("Fire1_2")) {
						FireProjectile ();
						num_projectiles++;
						timer = spam_time;
                        anim.SetBool("Attacking", true);

                        //Plays projectile sound
                        int clipNum = Random.Range (0, attackNum);
						if (gameObject.GetComponent<Entity> ().type == Entity.Element.bass) {
							attackSources [audioCount].clip = fishSFX [clipNum];
						}
						if (gameObject.GetComponent<Entity> ().type == Entity.Element.guitar) {
							attackSources [audioCount].clip = catSFX [clipNum];
						}
						if (gameObject.GetComponent<Entity> ().type == Entity.Element.horn) {
							attackSources [audioCount].clip = birdSFX [clipNum];
						}
						attackSources [audioCount].Play (0);
						audioCount++;
						if (audioCount >= attackNum) {
							audioCount = 0;
						}
					}
				}
			} else { //cannot fire; spam

			}
		}
	}
	void FireProjectile(){
        Entity.Element myType = gameObject.GetComponent<Entity>().type;
        if (gameObject.GetComponent<Entity>().type == Entity.Element.bass)
        {
            GameObject projectileClone = (GameObject)Instantiate(fishNote, spawnLocation.position, spawnLocation.rotation);
            projectileClone.GetComponent<Entity>().type = myType;
            projectileClone.GetComponent<SpriteRenderer>().sprite = fishNotes;
            projectileClone.GetComponent<ProjectileHandler>().speed = gameObject.transform.localScale.x * blasterSpeed;
            projectileClone.GetComponent<ProjectileHandler>().source = gameObject;
            projectileClone.transform.localScale = new Vector3(Mathf.Sign(gameObject.transform.localScale.x), projectileClone.transform.localScale.y, projectileClone.transform.localScale.z);
            projectileClone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(gameObject.transform.localScale.x)*16f, 8f);
        }
        if (gameObject.GetComponent<Entity>().type == Entity.Element.guitar)
        {
            GameObject projectileClone = (GameObject)Instantiate(catNote, spawnLocation.position, spawnLocation.rotation);
            projectileClone.GetComponent<Entity>().type = myType;
            projectileClone.GetComponent<SpriteRenderer>().sprite = catNotes;
            projectileClone.GetComponent<ProjectileHandler>().speed = gameObject.transform.localScale.x * blasterSpeed;
            projectileClone.GetComponent<ProjectileHandler>().source = gameObject;
            projectileClone.transform.localScale = new Vector3(Mathf.Sign(gameObject.transform.localScale.x), projectileClone.transform.localScale.y, projectileClone.transform.localScale.z);
        }
        if (gameObject.GetComponent<Entity>().type == Entity.Element.horn)
        {
            GameObject projectileClone = (GameObject)Instantiate(birdNote, spawnLocation.position, spawnLocation.rotation);
            projectileClone.GetComponent<Entity>().type = myType;
            projectileClone.GetComponent<SpriteRenderer>().sprite = birdNotes;
            projectileClone.GetComponent<ProjectileHandler>().speed = gameObject.transform.localScale.x * blasterSpeed;
            projectileClone.GetComponent<ProjectileHandler>().source = gameObject;
            projectileClone.transform.localScale = new Vector3(Mathf.Sign(gameObject.transform.localScale.x), projectileClone.transform.localScale.y, projectileClone.transform.localScale.z);
        }
    }
}
