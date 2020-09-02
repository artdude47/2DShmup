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
    public Transform firePoint2;
    public float shootTime = .5f;
    public GameObject textbox;
    public GameObject livesText;
    public GameObject victoryText;
    public AudioClip shootSound;
    public AudioClip levelEndSound;
    public GameObject teamShip;
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
    ArrayList myShips = new ArrayList();

    private int speedLevel = 1;
    private int shootLevel = 1;
    private int shipLevel = 1;
    private int numShips = 0;

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
            shipLevel = PlayerPrefs.GetInt("ShipLevel");
            shootLevel = PlayerPrefs.GetInt("ShootLevel");
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
            case 4:
                speed = 5.75f;
                break;
            case 5:
                speed = 6f;
                break;
        }

        switch (shipLevel)
        {
            case 1:
                break;
            case 2:
                numShips = 1;
                SpawnShip(numShips);
                break;
            case 3:
                numShips = 2;
                SpawnShip(numShips);
                break;
        }

        Debug.Log("Speed" + speedLevel);
        Debug.Log("Level" + level);
    }

    // Update is called once per frame
    void Update()
    {
        sr.material = matDefault;
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
        GameObject bul;
        GameObject bul2;
        GameObject bul3;
        Debug.Log(shootLevel);
        switch (shootLevel)
        {
            case 1:
                bul = Instantiate(bullet, firePoint.position, firePoint.rotation);
                bul.GetComponent<PlayerBullet>().SetPlayer(this.gameObject);
                AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
                break;
            case 2:
                bul = Instantiate(bullet, firePoint.position, firePoint.rotation);
                bul2 = Instantiate(bullet, firePoint2.position, firePoint2.rotation);
                bul.GetComponent<PlayerBullet>().SetPlayer(this.gameObject);
                bul2.GetComponent<PlayerBullet>().SetPlayer(this.gameObject);
                break;
            case 3:
                bul = Instantiate(bullet, firePoint.position, firePoint.rotation);
                bul.GetComponent<PlayerBullet>().angle = 1;
                bul2 = Instantiate(bullet, firePoint2.position, firePoint2.rotation);
                bul3 = Instantiate(bullet, firePoint.position, firePoint.rotation);
                bul2.GetComponent<PlayerBullet>().angle = -1;
                bul.GetComponent<PlayerBullet>().SetPlayer(this.gameObject);
                bul2.GetComponent<PlayerBullet>().SetPlayer(this.gameObject);
                bul3.GetComponent<PlayerBullet>().SetPlayer(this.gameObject);
                break;



        }
        AudioSource.PlayClipAtPoint(shootSound, firePoint.position);

        if(numShips > 0)
        {
            foreach(GameObject teamShip in myShips)
            {
                Instantiate(bullet, teamShip.transform.position, teamShip.transform.rotation);
            }
        }
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
            PlayerPrefs.SetInt("ShootLevel", shootLevel);
            PlayerPrefs.SetInt("ShipLevel", shipLevel);
            AudioSource.PlayClipAtPoint(levelEndSound, transform.position);
            victoryText.SetActive(true);
            if (level != 9)
            {
                WaitForEnd();
            }
            else
            {
                Victory();
            }

            
        }
        if (collision.tag == "1up")
        {
            AddLives();
            AudioSource.PlayClipAtPoint(powerUp, transform.position);
            Destroy(collision.gameObject);
        }
    }

    private void WaitForEnd()
    {
        victoryText.SetActive(true);
        Invoke("EndLevel", 5);
    }

    private void Victory()
    {
        victoryText.GetComponent<Text>().text = "VICTORY!!!";
        victoryText.SetActive(true);
    }

    private void EndLevel()
    {
        SceneManager.LoadScene("Shop");
    }
    private void AddLives()
    {
        lives += 1;
        Debug.Log("Lives Added!");
    }

    private void SpawnShip(int numShips)
    {
        Debug.Log("Spawning " + numShips + " ship(s)!");
        GameObject ship1;
        GameObject ship2;

        if(numShips == 1)
        {
            ship1 = Instantiate(teamShip, new Vector3(transform.position.x - 1, transform.position.y + 2, transform.position.z), transform.rotation);
            ship1.transform.parent = this.transform;
            myShips.Add(ship1);
        } else if(numShips == 2)
        {
            ship1 = Instantiate(teamShip, new Vector3(transform.position.x - 1, transform.position.y + 2, transform.position.z), transform.rotation);
            ship1.transform.parent = this.transform;
            ship2 = Instantiate(teamShip, new Vector3(transform.position.x - 1, transform.position.y - 2, transform.position.z), transform.rotation);
            ship2.transform.parent = this.transform;
            myShips.Add(ship1);
            myShips.Add(ship2);
        }
    }

}
