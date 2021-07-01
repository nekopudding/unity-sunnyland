using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleShot : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    //[SerializeField] private Transform player;
    private Rigidbody2D rb;
    

    IEnumerator Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left*speed;

        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

}
