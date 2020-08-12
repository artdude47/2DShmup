using System.Collections;
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
    private int level = 0;

    private float nextShoot = 0f;

    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private float currentShoot = 0f;
    private int score = 0;
    private int money = 0;
    private Vector2 spawnCoods;
    public int immunityTimer = 0;
    public AudioClip powerUp;

    private int speedLevel = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnCoods = transform.position;
        if(PlayerPrefs.GetInt("Level") != 0)
        {
            money = PlayerPrefs.GetInt("Money");
            level = PlayerPrefs.GetInt("Level");
            speedLevel = PlayerPrefs.GetInt("SpeedLevel");
        }

        switch (speedLevel)
        {
            case 1:
                speed = 5f;
                break;
            case 2:
                speed = 5.25f;
                break;
            case 3:
                speed = 5.5f;
                break;
        }

        Debug.Log("Speed" + speedLevel);
        Debug.Log("Level" + level);
    }

    // Update is called once per frame
    void Update()
    {
        if (immunityTimer > 0) immunityTimer--;
        if(livesText != null) livesText.GetComponent<Text>().text = "Lives: " + lives;
        if(textbox != null) textbox.GetComponent<Text>().text = "Score: " + score;
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

    public void ScoreUp(int newScore, int newMoney)
    {
        score += newScore;
        money += newMoney;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Respawn()
    {
        transform.position = spawnCoods;
        this.gameObject.SetActive(true);
        sr.material = matDefault;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Win")
        {
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.SetInt("SpeedLevel", speedLevel);
            SceneManager.LoadScene("Shop");
            
        }
        if (collision.tag == "1up")
        {
            AddLives();
            AudioSource.PlayClipAtPoint(powerUp, transform.position);
            Destroy(collision.gameObject);
        }
    }

    private void AddLives()
    {
        lives += 1;
        Debug.Log("Lives Added!");
    }

}
