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
    [SerializeField] private float jumpForce = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update() {
        float hDirection = Input.GetAxis("Horizontal");
        if(hDirection < 0 ) {
            rb.velocity = new Vector2(-speed,rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        else if(hDirection > 0) {
            rb.velocity = new Vector2(speed,rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)) {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            state = State.jumping;
        }

        VelocityState();
        animator.SetInteger("state", (int) state);
    }

    private void VelocityState()
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
