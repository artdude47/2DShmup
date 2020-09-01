using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Enemy
{
    public Transform player;
    public Transform UpShoot;
    public Transform LeftShoot;
    public Transform RightShoot;
    public GameObject chaseBullet;
    public float speed = 5f;

    private Animator anim;
    private Rigidbody2D rb;

    private int timer = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (transform.position.x < 8.4)
            timer++;
        {
            if (timer == 270)
            {
                Shoot();
                timer = 0;
            }
        }

        //check player's position vs. current position
        if (player.position.x <= transform.position.x - 3)
        {
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
        } else if (player.position.x >= transform.position.x + 3)
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", true);
        }
        else
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-1 * speed, 0);
    }

    private void Shoot()
    {
        GameObject bull = Instantiate(chaseBullet, transform.position, transform.rotation);
        Bullet bullScript = bull.GetComponent<Bullet>();
        bullScript.rb.velocity = (player.transform.position - transform.position).normalized * bullScript.speed;
    }
}
