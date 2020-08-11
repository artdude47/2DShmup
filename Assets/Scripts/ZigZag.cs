using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZag : Enemy
{
    public float speed = 5f;
    public float vertSpeed = 2.5f;
    private int dir = 1;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y > 4.8) dir = -1;
        if (transform.position.y < -4) dir = 1;
        rb.velocity = new Vector2(speed * -1, vertSpeed * dir);
    }
}
