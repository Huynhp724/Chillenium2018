using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour {

    public GameObject source;
    public GameObject explosion;

	public float speed = 5f;
	public float death_timer = 1f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = death_timer;
	}
	
	// Update is called once per frame
	void Update () {
		if(source.gameObject.GetComponent<Entity>().type != Entity.Element.horn)transform.Translate (speed * Time.deltaTime, 0, 0);

		//TIMER
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			Destroy (gameObject);
		}
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject == source)
			return;
        if (col.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("MAKING IT");
            GameObject explode = (GameObject)Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        if (col.gameObject.GetComponent<Entity>() == null)
        {
            Debug.Log("Collider doesn't have Entity");
            return;
        }
		Entity.Element myType = gameObject.GetComponent<Entity>().type;
		if (col.gameObject.tag.Equals ("Player")) {
			Debug.Log ("hit player");
			col.gameObject.GetComponent<PlayerChar> ().DamagePlayer (source, myType); 
			Destroy (gameObject);
			return;
		}
		Entity.Element otherType = col.gameObject.GetComponent<Entity>().type;		
        
		Debug.Log ("collision with " + otherType);

		if (otherType == myType) { //if same type, destroy both
            GameObject explode = (GameObject)Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy (gameObject);
		}

		//RPS:
		//BASS beats GUITAR beats HORN beats...
		if (myType == Entity.Element.guitar) { //if col is guitar
			if (otherType == Entity.Element.bass) {
				Debug.Log ("guitar lost to bass");
				Destroy (gameObject);		
			}
		}
		else if (myType == Entity.Element.bass) { //if col is bass
			if (otherType == Entity.Element.horn) {	
				Debug.Log ("bass lost to horn");
				Destroy (gameObject);
			}
		}
		else if (myType == Entity.Element.horn) { //if col is horn
			if (otherType == Entity.Element.guitar){	
				Debug.Log ("horn lost to guitar");
				Destroy (gameObject);
			}
		}
	}
}
