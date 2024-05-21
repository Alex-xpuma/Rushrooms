using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundHeight;
    BoxCollider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider.size.y / 2)+1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
