using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.Build;

public class Ship : MonoBehaviour
{

    public int health;
    public GameObject explosion;
    private int impactDamage = 1;
    public AudioClip explode;


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
  
    }

    protected void Die()
    {
        //if it is the player turn off the game object and play animation
        bool isPlayer = false;
        if (this.GetComponent<PlayerMovement>())
        {
            isPlayer = true;
        }

        if (isPlayer)
        {
            GameObject tempPlayer = GetComponent<PlayerMovement>().gameObject;
            PlayerMovement playerScript = GetComponent<PlayerMovement>();
            if (tempPlayer.GetComponent<PlayerMovement>().lives > 0 && playerScript.immunityTimer == 0)
            {
                playerScript.immunityTimer = 20;
                tempPlayer.SetActive(false);
                playerScript.lives--;
                playerScript.Respawn();
            }
            else if(playerScript.lives <= 0)
            {
                playerScript.lives--;
                Destroy(gameObject);
                GameOver();
            }
        }
        else
        {
            Destroy(gameObject);
        }
        AudioSource.PlayClipAtPoint(explode, transform.position);
        Instantiate(explosion, transform.position, transform.rotation);
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
        SceneManager.LoadScene("GameOver");
    }
}
