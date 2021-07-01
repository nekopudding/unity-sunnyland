using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject bulletPrefab;
    private float shootRange = 20f;
    private float shootCD = 3f;

    new void Start() {
        base.Start();
        InvokeRepeating("Shoot", 0f, shootCD);
    }

    private void Shoot() {
        if(Vector2.Distance(player.position, firePoint.position) < shootRange) {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    
}
