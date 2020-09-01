using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RamEnemy : Enemy
{

    public float speed = 5f;
    public float ramSpeed = 30f;
    public Transform player;

    int phase = 1;
    private Rigidbody2D rb;

    /*During phase 1 try to match player's y position...Once matched switch to 
     * phase 2. On phase 2 try to ram player by quickly moving towards player's x
     * position. */


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //first phase tries to track the player
        if (phase == 1)
        {
            //check to make sure enemy is on screen
            if (transform.position.x < 8.2 && transform.position.x <= player.transform.position.x + 6)
            {
                //check what current y value is versus player's and try to match player
                if (transform.position.y > player.position.y) MoveDown();
                if (transform.position.y < player.position.y) MoveUp();

                //when close to player
                if (transform.position.y >= player.position.y && transform.position.y <= player.position.y + .5)
                {
                    SwitchPhase();
                }
                if (transform.position.y <= player.position.y && transform.position.y >= player.position.y - .5)
                {
                    SwitchPhase();
                }
            } else
            {
                rb.velocity = new Vector2(-1 * speed, 0);
            }
        }

        //second phase tries to ram the player
        if (phase == 2) Ram();

        
    }

    void MoveDown()
    {
        rb.velocity = new Vector2(0, -1 * speed);
    }

    void MoveUp()
    {
        rb.velocity = new Vector2(0, 1 * speed);
    }

    void SwitchPhase()
    {
        phase = 2;
        rb.velocity = new Vector2(0, 0);
    }

    void Ram()
    {
        rb.velocity = new Vector2(-1 * ramSpeed, 0);
    }
}
