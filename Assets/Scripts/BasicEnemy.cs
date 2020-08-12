using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicEnemy : Enemy
{

    public float speed = 5f;
    public float vertSpeed = 7f;
    public GameObject bullet;
    public Transform firePoint;
    public int shotChance = 300;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDirection();

    }

    // Update is called once per frame
    void Update()
    {
        /* No longer shoot for now?
        //generate bullets randomly
        int randomNum = UnityEngine.Random.Range(0, shotChance);
        if (randomNum == 2 && transform.position.x < 13) Shoot();
        */

        if (transform.position.x < -1) ChangeDirection();

    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector2(speed * -1, 0);
    }

    private void Shoot()
    {
        Instantiate(bullet, firePoint.position, transform.rotation);
    }

    private void ChangeDirection()
    {
        if (transform.position.y > 1)
        {
            rb.velocity = new Vector2(0, vertSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, vertSpeed * -1);
        }
    }

    private void SetDirection()
    {

        rb.velocity = new Vector2(speed * -1, 0);
    }

}
