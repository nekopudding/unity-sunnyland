using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        transform.position = new Vector3(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y, transform.position.z);
    }
}
