using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;


    private enum State {idle, running, jumping, falling};
    private State state = State.idle;
    [SerializeField] private int speed = 7;
    [SerializeField] private float airControl = 0.2f;
    [SerializeField] private float jumpForce = 20f;

    public int gems = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        
        Movement();

        AnimationState();
        animator.SetInteger("state", (int)state); //set animation based on enumerator state
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            gems++;
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
        if (hDirection < 0 && state == State.falling)
        {
            rb.AddForce(new Vector2(-airControl, 0f));
        }
        else if (hDirection > 0 && state == State.falling)
        {
            rb.AddForce(new Vector2(airControl, 0f));
        }

        //moving left
        else if (hDirection < 0 && isTouchingGround)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }

        //moving right
        else if (hDirection > 0 && isTouchingGround)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }

        //jumping
        if (jumping && isTouchingGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }
    }

    /**
     * Function that transitions between animation states as defined in the enumerator
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
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}
