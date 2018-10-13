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
    public bool change = false; 
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public Transform SpawnLocation;
    Entity.Element type;
    Entity.Element changeType;

    Rigidbody2D rb;
    SpriteRenderer spr;
    Animator anim;
    Entity ent;
    AudioSource aud;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        type = gameObject.GetComponent<Entity>().type;
        ent = gameObject.GetComponent<Entity>();
        aud = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if ((Input.GetButtonDown("Jump")||Input.GetAxisRaw("Vertical") == 1) && grounded)
        {
            jumped = true;
        }
        //Debug.Log(Input.GetAxisRaw("DpadX"));
        if (GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>().canChange)
        {
            if (Input.GetAxisRaw("DpadX") < 0)
            {
                changeType = Entity.Element.bass;
                GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>().checkIn = true;

            }
            if (Input.GetAxisRaw("DpadX") > 0)
            {
                changeType = Entity.Element.horn;
                GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>().checkIn = true;

            }
            if (Input.GetAxisRaw("DpadY") > 0)
            {
                changeType = Entity.Element.guitar;
                GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>().checkIn = true;

            }
           
                            
        }

        //TRANSFORMATION
        if (change)
        {
            transformation();
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
           
            rb.velocity = new Vector2(0.0f, jumpForce);
            jumped = false;
        }

        //flip if moving other way
        if(move < 0 && facingRight)
        {
            //spr.flipX = true;
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

    public void changeRumble()
    {
        Debug.Log("NO RUMBLE");
        anim.SetBool("Rumbling", false);
    }

    public void transformation()
    {
        if (type != changeType)
        {
            type = changeType;
            if (type == Entity.Element.bass)
            {
                anim.SetInteger("Form", -1);
            }
            if (type == Entity.Element.guitar)
            {
                anim.SetInteger("Form", 0);
            }
            if (type == Entity.Element.horn)
            {
                anim.SetInteger("Form", 1);
            }
            anim.SetBool("Rumbling", true);
            change = false;
        }
    }
}
