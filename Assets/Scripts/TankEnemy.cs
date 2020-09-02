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

    private bool shooting = false;
    private Animator anim;
    private Rigidbody2D rb;

    private int timer = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        InvokeRepeating("CheckShoot", 1.5f, 1.5f);
    }
    private void Update()
    {
        if (transform.position.x < 8.4 && shooting == false)
            timer++;
        shooting = true;
        {
            if (timer == 150)
            {
               // Shoot();
                timer = 0;
            }
            
        }

        if (transform.position.x < -9) Die(false);

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

    private void CheckShoot()
    {
        if(transform.position.x < 8.75)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bull = Instantiate(chaseBullet, transform.position, transform.rotation);
        Bullet bullScript = bull.GetComponent<Bullet>();
        bullScript.rb.velocity = (player.transform.position - transform.position).normalized * bullScript.speed;
    }
}
