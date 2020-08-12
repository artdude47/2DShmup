using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ship
{
    public int scoreCount;
    public int moneyCount;


    private void Update()
    {
        if (transform.position.x < -10) Die(false);
    }

}
