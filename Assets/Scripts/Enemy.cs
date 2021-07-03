using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource explosion;

    protected virtual void Start() //allows children to have access, virtual allows override
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        explosion = GetComponent<AudioSource>();
    }
    public void JumpedOn()
    {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Death");
        explosion.Play();

    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
