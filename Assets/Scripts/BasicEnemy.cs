using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{

    public float speed = 5f;
    public GameObject bullet;
    public Transform firePoint;
    public int shotChance = 300;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //generate bullets randomly
        int randomNum = UnityEngine.Random.Range(0, shotChance);
        if (randomNum == 2 && transform.position.x < 13) Shoot();

        if (transform.position.x < -10) Die();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * -1, 0);
    }

    private void Shoot()
    {
        Instantiate(bullet, firePoint.position, transform.rotation);
    }


}
