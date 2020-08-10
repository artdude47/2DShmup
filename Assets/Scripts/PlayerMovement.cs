﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : Ship
{

    public float speed = 5f;
    public GameObject bullet;
    public Transform firePoint;
    //public Transform firePoint2;
    public float shootTime = .5f;
    public GameObject textbox;
    public GameObject livesText;
    public AudioClip shootSound;
    public int lives;

    private float nextShoot = 0f;

    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private float currentShoot = 0f;
    private int score = 0;
    private Vector2 spawnCoods;
    public int immunityTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnCoods = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (immunityTimer > 0) immunityTimer--;
        livesText.GetComponent<Text>().text = "Lives: " + lives;
        textbox.GetComponent<Text>().text = "Score: " + score;
        //counter to limit shooting speed
        if (currentShoot > 0) currentShoot--;

        //horizontal and verticle movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
        MovePlayer(horizontalMove, verticalMove);

        if (Input.GetButton("Shoot"))
        {
            if (Time.time > nextShoot)
            {
                nextShoot = Time.time + shootTime;
                Shoot();
            }
        }

        if(lives <= 0)
        {
            GameOver();
        }
        
    }

    private void MovePlayer(float hor, float vert)
    {
        rb.velocity = new Vector2(hor, vert);
    }

    private void Shoot()
    {
        GameObject bul = Instantiate(bullet, firePoint.position, firePoint.rotation);
        bul.GetComponent<PlayerBullet>().SetPlayer(this.gameObject);
        AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
    }

    public void ScoreUp(int newScore)
    {
        score += newScore;
    }

    private void GameOver()
    {
        SceneManager.LoadScene(1);
    }

    public void Respawn()
    {
        transform.position = spawnCoods;
        this.gameObject.SetActive(true);
    }

}
