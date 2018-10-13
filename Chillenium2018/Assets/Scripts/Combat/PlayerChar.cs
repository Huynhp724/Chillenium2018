using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerChar : MonoBehaviour {

    public enum Element{bass, guitar, horn};
    public Element type;
    public int maxHealth = 10;
    public int health = 10;
    public float blasterSpeed = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
