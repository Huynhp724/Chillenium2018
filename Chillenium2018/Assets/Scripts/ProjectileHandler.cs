using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour {

	public enum TypeName {
		fire, water, grass
	};

	public TypeName type;

	public float speed = 5f;
	public float death_timer = 1f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (speed * Time.deltaTime, 0, 0);

		//TIMER
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("collision");
		Destroy (gameObject);
	}
}
