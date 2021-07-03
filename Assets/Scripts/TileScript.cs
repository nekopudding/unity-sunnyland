using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{
    [SerializeField] private Tile coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Tile>();
        coll.colliderType = Tile.ColliderType.Sprite;
    }
}
