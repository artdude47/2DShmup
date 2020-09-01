﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public int health;
    public GameObject explosion;
    public GameObject ExtraLife;
    private int impactDamage = 1;
    public AudioClip explode;

    public Material matDefault;
    public Material matWhite;
    public SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        sr.material = matWhite;
        if (health <= 0)
        {
            Die(true);
        } else
        {
            Invoke("ResetMaterial", .1f);
        }
  
    }

    protected void Die(bool playSound)
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
                playerScript.sr.material = matDefault;
                ResetMaterial();
                playerScript.Respawn();
                ResetMaterial();
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
            DropChance();
            Destroy(gameObject);
        }
        if(playSound) AudioSource.PlayClipAtPoint(explode, transform.position);
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

    //5% chance for 1 up drop
    private void DropChance()
    {
        float tempNum = Random.Range(0, 10);
        if (tempNum == 2)
        {
            Instantiate(ExtraLife, transform.position, new Quaternion(0,0,0,0));
        }
    }

    public void ResetMaterial()
    {
        sr.material = matDefault;
    }
}
