using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private int money;
    private int speedLevel;
    private int shipLevel;
    private int shootLevel;
    private int level;
    public GameObject money_Text;
    public GameObject speed_Cost_Text;
    public GameObject speed_Bar;
    public GameObject speed_Button;

    public GameObject shoot_Cost_Text;
    public GameObject shoot_Bar;
    public GameObject shoot_Button;

    public GameObject ship_Cost_Text;
    public GameObject ship_Bar;
    public GameObject ship_Button;
    public AudioClip selectSound;

    private int speedCost;
    private int shootCost;
    private int shipCost;

    private void Awake()
    {
        money = PlayerPrefs.GetInt("Money");
        level = PlayerPrefs.GetInt("Level");
        speedLevel = PlayerPrefs.GetInt("SpeedLevel");
        shipLevel = PlayerPrefs.GetInt("ShipLevel");
        shootLevel = PlayerPrefs.GetInt("ShootLevel");
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        money_Text.GetComponent<Text>().text = "Money: " + money;
    }

    public void BuySpeed()
    {
        if(money >= speedCost)
        {
            AudioSource.PlayClipAtPoint(selectSound, new Vector3(0, 0, 0));
            speedLevel++;
            money -= speedCost;
            UpdateMoney();
        }
    }

    public void BuyShoot()
    {
        if(money >= shootCost)
        {
            AudioSource.PlayClipAtPoint(selectSound, new Vector3(0, 0, 0));
            shootLevel++;
            money -= shootCost;
            UpdateMoney();
        }
    }

    public void BuyShip()
    {
        if (money >= shipCost)
        {
            AudioSource.PlayClipAtPoint(selectSound, new Vector3(0, 0, 0));
            shipLevel++;
            money -= shipCost;
            UpdateMoney();
        }
    }

    private void UpdateMoney()
    {
        switch (speedLevel)
        {
            case 1:
                speedCost = 100;
                speed_Bar.GetComponent<Slider>().value = 0;
                break;
            case 2:
                speedCost = 1075;
                speed_Bar.GetComponent<Slider>().value = .25f;
                break;
            case 3:
                speedCost = 2500;
                speed_Bar.GetComponent<Slider>().value = .5f;
                break;
            case 4:
                speedCost = 4000;
                speed_Bar.GetComponent<Slider>().value = .75f;
                break;
            case 5:
                speedCost = 6000;
                speed_Bar.GetComponent<Slider>().value = 1.0f;
                speed_Button.GetComponent<Button>().interactable = false;
                break;
           
        }
        speed_Cost_Text.GetComponent<Text>().text = "$" + speedCost;

        switch(shootLevel)
        {
            case 1:
                shootCost = 500;
            shoot_Bar.GetComponent<Slider>().value = 0;
            break;
            case 2:
                shootCost = 2500;
            shoot_Bar.GetComponent<Slider>().value = .5f;
            break;
            case 3:
                shootCost = 0000;
            shoot_Bar.GetComponent<Slider>().value = 1.0f;
                shoot_Button.GetComponent<Button>().interactable = false;
                break;
        }
        shoot_Cost_Text.GetComponent<Text>().text = "$" + shootCost;

        switch (shipLevel)
        {
            case 1:
                shipCost = 1000;
                ship_Bar.GetComponent<Slider>().value = 0;
                break;
            case 2:
                shipCost = 4000;
                ship_Bar.GetComponent<Slider>().value = .50f;
                break;
            case 3:
                shipCost = 0000;
                ship_Bar.GetComponent<Slider>().value = 1.0f;
                ship_Button.GetComponent<Button>().interactable = false;
                break;

        }
        ship_Cost_Text.GetComponent<Text>().text = "$" + shipCost;
    }

    public void LoadNextLevel()
    {
        AudioSource.PlayClipAtPoint(selectSound, new Vector3(0, 0, 0));
        level++;
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("SpeedLevel", speedLevel);
        PlayerPrefs.SetInt("ShootLevel", shootLevel);
        PlayerPrefs.SetInt("ShipLevel", shipLevel);
        SceneManager.LoadScene(level + 1);
    }
}
