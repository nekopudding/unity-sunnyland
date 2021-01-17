using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;


    private void Update() {
        if(Input.GetKey(KeyCode.A)) {
            rb.velocity = new Vector2(-5,rb.velocity.y);
        }
        if(Input.GetKey(KeyCode.D)) {
            rb.velocity = new Vector2(5,rb.velocity.y);
        }
        
        if(Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x,10f);
        }
        

    }
}
