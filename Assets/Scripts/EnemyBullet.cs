using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * -1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            Die();
        }
        if (collision.GetComponent<EnemyBullet>())
        {
            Die();
        }

        if (collision.GetComponent<BasicEnemy>()) Die();
    }
}
