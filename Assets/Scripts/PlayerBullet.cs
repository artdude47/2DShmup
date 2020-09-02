﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

    public GameObject player;
    private PlayerMovement playerScript;
    public int angle = 0;

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, angle);
    }

    private void Update()
    {
        if (transform.position.x > 9) Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*  if (collision.GetComponent<BasicEnemy>())
          {
              collision.GetComponent<BasicEnemy>().TakeDamage(damage);
              playerScript.ScoreUp(collision.GetComponent<BasicEnemy>().scoreCount);
              Die();
          } */
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Ship>().TakeDamage(damage);
            playerScript.ScoreUp(collision.GetComponent<Enemy>().scoreCount, collision.GetComponent<Enemy>().moneyCount);
            Die();
        }

    }

    public void SetPlayer(GameObject setPlayer)
    {
        player = setPlayer;
        playerScript = player.GetComponent<PlayerMovement>();
    }
}
