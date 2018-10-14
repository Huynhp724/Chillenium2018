using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(Death());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
