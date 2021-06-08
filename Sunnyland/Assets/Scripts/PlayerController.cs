using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private Collider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource gemAudio;
    [SerializeField] private AudioSource jumpAudio;



    private enum State {idle, running, jumping, falling, hurt}; //animation states, decides interactions
    private State state = State.idle;
    [SerializeField] private int speed = 7;
    //[SerializeField] private float airControl = 3f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float airControl = 0.6f;
    [SerializeField] private int gems = 0;
    [SerializeField] private Text gemText;
    [SerializeField] private float hurtForce = 5f;
     

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
       if (state != State.hurt)
        {
            Movement();
        } 
        

        AnimationState();
        animator.SetInteger("state", (int)state); //set animation based on enumerator state
    }


    //collectible collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            gemAudio.Play();
            Destroy(collision.gameObject);
            gems++;
            gemText.text = gems.ToString();
        }
    }

    //enemy collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x) 
                {
                    rb.velocity = new Vector2(-hurtForce, 0f);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, 0f);
                }
            }
        }
    }
    /**
     * Function that controls movement based on input values. 
     **/
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        bool jumping = Input.GetButtonDown("Jump");
        bool isTouchingGround = coll.IsTouchingLayers(ground);

        //midair movement
        if (hDirection < 0 && !isTouchingGround)
        {
            rb.velocity = new Vector2(-speed*airControl, rb.velocity.y);
        }
        else if (hDirection > 0 && !isTouchingGround)
        {
            rb.velocity = new Vector2(speed*airControl, rb.velocity.y);
        }

        //moving left
        else if (hDirection < 0 && isTouchingGround)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);

        }

        //moving right
        else if (hDirection > 0 && isTouchingGround)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        //staying still
        else if (hDirection == 0 && isTouchingGround)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //jumping
        if (jumping && isTouchingGround)
        {
            Jump();
        }

        if (hDirection <0)
        {
            transform.localScale = new Vector2(-1, 1); //sets sprite horizontal flip
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
        
        
    }

    
    private void Jump()
    {
        jumpAudio.Play();
        rb.velocity = new Vector2(rb.velocity.x/2, jumpForce);
        state = State.jumping;
    }
    /**
     * Function that transitions between animation states as defined in the enumerator
     * animation is set in Update
     **/
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 2f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        footstep.Play();
    }
}
