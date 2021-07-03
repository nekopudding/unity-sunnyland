using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField] private Transform firePoint;
    private Transform player;
    [SerializeField] private GameObject bulletPrefab;
    private float shootRange = 12f; //should match radius of trigger on shot script
    [SerializeField] private float shootCD = 3f;

    new void Start() {
        base.Start();
        player = GameObject.Find("Player").transform;
        InvokeRepeating("Shoot", 0f, shootCD);
    }

    private void Shoot() {
        if(Vector2.Distance(player.position, firePoint.position) < shootRange) {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    
}
