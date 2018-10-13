using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public float gravityForce;
	// Use this for initialization
	void Start () {
        Physics2D.gravity = new Vector3(0.0f, gravityForce, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
