using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineEnemy : Enemy
{


    public float Frequency = 20f;
    public float Magnitude = .5f;
    public float speed = 5.0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(-1 * speed, 1 * Mathf.Sin(Time.time *Frequency) * Magnitude);
    }
}
