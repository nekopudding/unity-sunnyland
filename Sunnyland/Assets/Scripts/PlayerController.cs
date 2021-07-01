using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource gemAudio;
    [SerializeField] private AudioSource jumpAudio;



    private enum State {idle, running, jumping, falling, hurt}; //animation states, decides interactions
    private State state = State.idle;
    private int speed = 9;
    //[SerializeField] private float airControl = 3f;
    private float jumpForce = 25f;
    private float airControl = 0.8f;
    private float hurtForce = 10f;
     

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
        
        animator.SetInteger("state", (int)state); //set animation based on enumerator state
        AnimationState();
        
    }


    //trigger collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            gemAudio.Play();
            Destroy(collision.gameObject);
            PermanentUI.perm.gems++;
            PermanentUI.perm.gemText.text = PermanentUI.perm.gems.ToString();
        }
        if (collision.tag == "Enemy") //enemy projectiles
        {
                state = State.hurt;
                if (collision.gameObject.transform.position.x > transform.position.x) 
                {
                    rb.velocity = new Vector2(-hurtForce, hurtForce);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, hurtForce);
                }
        }
    }

    //enemy collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            
            RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, 1.3f, EnemyLayer);
            if (hit.collider != null || state == State.falling)
            {
                state = State.falling;
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x) 
                {
                    rb.velocity = new Vector2(-hurtForce, hurtForce);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, hurtForce);
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
        if(state != State.hurt) {
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
        }
        //jumping
        if (jumping)
        {
            RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, 0.8f, ground);
            if (hit.collider != null)
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
