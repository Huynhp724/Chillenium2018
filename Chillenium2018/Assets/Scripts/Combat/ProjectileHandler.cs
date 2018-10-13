using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour {

    public GameObject source;

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
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject == source)
			return;
        if (col.gameObject.GetComponent<Entity>() == null)
        {
            Debug.Log("Collider doesn't have Entity");
            return;
        }
		if (col.gameObject.tag.Equals ("Player")) {
			Debug.Log ("hit player");
			Destroy (gameObject);
			return;
		}
        Entity.Element myType = gameObject.GetComponent<Entity>().type;
		Entity.Element otherType = col.gameObject.GetComponent<Entity>().type;		
        
		Debug.Log ("collision with " + otherType);

        if (col.gameObject != source)Destroy (gameObject);
	}
}
