using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{

    Rigidbody2D rb;
    private int dir = 1;
    private int shootTimer = 0;

    public GameObject bullet;
    public Transform firePos;
    public GameObject win;
    public int speed = 5;
    private int phase = 1;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= 6.75) phase = 2;
        if (phase == 2)
        {
            shootTimer++;
            if (shootTimer >= 150)
            {
                Shoot();
                shootTimer = 0;
            }
        }

        if (health <= 0) Die();
    }

    private void FixedUpdate()
    {
        if (phase == 1)
        {
            rb.velocity = new Vector2(-5, 0);
        }
        else
        {
            if (transform.position.y <= -2.7)
            {
                FlipDir();
            }
            if (transform.position.y > 2.7)
            {
                FlipDir();
            }

            rb.velocity = new Vector2(0, dir * speed);
        }
    }

    private void FlipDir()
    {
        dir = dir *-1;
    }

    private void Shoot()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject bul = Instantiate(bullet, firePos.position, firePos.rotation);
            Rigidbody2D rb = bul.GetComponent<Rigidbody2D>();

            switch (i)
            {
                case 0:
                    rb.velocity = new Vector2(-5, -1 * 5);
                    break;
                case 1:
                    rb.velocity = new Vector2(-5, -2);
                    break;
                case 2:
                    rb.velocity = new Vector2(-5, 0);
                    break;
                case 3:
                    rb.velocity = new Vector2(-5, 2);
                    break;
                case 4:
                    rb.velocity = new Vector2(-5, 5);
                    break;
            }
        }
    }

    private void Die()
    {
        Instantiate(win, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), transform.rotation);
    }

}
