using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public int health;
    public GameObject explosion;
    private int impactDamage = 1;
    public AudioClip explode;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    protected void Die()
    {
        bool isPlayer = false;
        if (this.GetComponent<PlayerMovement>())
        {
            isPlayer = true;
        }
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(explode, transform.position);
        Instantiate(explosion, transform.position, transform.rotation);
        if (isPlayer) GameOver();
    }

    //check for collisions among ships
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(impactDamage);
            TakeDamage(impactDamage);
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(1);
    }
}
