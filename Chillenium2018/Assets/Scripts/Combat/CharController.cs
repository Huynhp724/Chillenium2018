using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    float move; //what direction char is facing
    public float movespeed = 10f; //movespeed
    public float jumpForce = 10f;
    public float airSpeed = 2f;
    bool facingRight = true; //direction char is facing
    bool jumped = false; //turns to true when button is pressed
    bool grounded = false; //is Player in air
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public Transform SpawnLocation;

    Rigidbody2D rb;
    SpriteRenderer spr;
    Animator anim;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Debug.Log("Jump");
            jumped = true;
        }
	}

    private void FixedUpdate()
    {
        //Debug.Log(grounded);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        move = Input.GetAxis("Horizontal");
        //Moves Player left and right
        if (grounded)
        {
            anim.SetBool("Grounded", true);
            rb.velocity = new Vector2(move * movespeed, rb.velocity.y);
        }
        else
        {
            anim.SetBool("Grounded", false);
            rb.velocity = new Vector2(move * movespeed/airSpeed, rb.velocity.y);
        }
        anim.SetFloat("Speed", Mathf.Abs(move));

        //Jump
        if (jumped)
        {
            Debug.Log("JUMP");
            rb.velocity = new Vector2(0.0f, jumpForce);
            jumped = false;
        }

        //flip if moving other way
        if(move < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(
          transform.localScale.x * -1,
          transform.localScale.y,
          transform.localScale.z);
        }
        if(move > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
        }
    }
}
