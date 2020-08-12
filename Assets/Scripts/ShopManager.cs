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
    private int level;
    public GameObject money_Text;
    public GameObject speed_Cost_Text;
    public GameObject speed_Bar;
    public AudioClip selectSound;

    private int speedCost;

    private void Awake()
    {
        money = PlayerPrefs.GetInt("Money");
        level = PlayerPrefs.GetInt("Level");
        speedLevel = PlayerPrefs.GetInt("SpeedLevel");
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
                speed_Bar.GetComponent<Slider>().value = .10f;
                break;
            case 3:
                speedCost = 2500;
                speed_Bar.GetComponent<Slider>().value = .20f;
                break;
           
        }
        speed_Cost_Text.GetComponent<Text>().text = "$" + speedCost;
    }

    public void LoadNextLevel()
    {
        AudioSource.PlayClipAtPoint(selectSound, new Vector3(0, 0, 0));
        level++;
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("SpeedLevel", speedLevel);
        SceneManager.LoadScene(level + 1);
    }
}
