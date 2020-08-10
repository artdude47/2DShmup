using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxAttempt : MonoBehaviour
{
    public GameObject background;
    public GameObject background2;
    public GameObject background3;
    public GameObject background4;

    public GameObject background_1;
    public GameObject background2_1;
    public GameObject background3_1;
    public GameObject background4_1;

    public float backgroundSpeed = .01f;
    public float background2Speed = .05f;
    public float background3Speed = .1f;
    public float background4Speed = .2f;

    public int scrolltime = 5;
    private int timescroll = 100;

    private void Start()
    {
        timescroll = scrolltime;
    }

    private void FixedUpdate()
    {
        timescroll--;
        if (timescroll <= 0)
        {
            background.transform.position = new Vector3(background.transform.position.x - backgroundSpeed, background.transform.position.y, 0);
            background_1.transform.position = new Vector3(background_1.transform.position.x - backgroundSpeed, background_1.transform.position.y, 0);

            background2.transform.position = new Vector3(background2.transform.position.x - background2Speed, background2.transform.position.y, 0);
            background2_1.transform.position = new Vector3(background2_1.transform.position.x - background2Speed, background2_1.transform.position.y, 0);

            background3.transform.position = new Vector3(background3.transform.position.x - background3Speed, background3.transform.position.y, 0);
            background3_1.transform.position = new Vector3(background3_1.transform.position.x - background3Speed, background3_1.transform.position.y, 0);


            background4.transform.position = new Vector3(background4.transform.position.x - background4Speed, background4.transform.position.y, 0);
            background4_1.transform.position = new Vector3(background4_1.transform.position.x - background4Speed, background4_1.transform.position.y, 0);
            timescroll = scrolltime;
        }

        //make infinite
        if (background.transform.position.x < -32) background.transform.position = new Vector3(43, background.transform.position.y, 0);
        if (background_1.transform.position.x < -32) background_1.transform.position = new Vector3(43, background_1.transform.position.y, 0);

        if (background2.transform.position.x < -36) background2.transform.position = new Vector3(48, background2.transform.position.y, 0);
        if (background2_1.transform.position.x < -36) background2_1.transform.position = new Vector3(48, background2_1.transform.position.y, 0);

        if (background3.transform.position.x < -27) background3.transform.position = new Vector3(35, background3.transform.position.y, 0);
        if (background3_1.transform.position.x < -27) background3_1.transform.position = new Vector3(35, background3_1.transform.position.y, 0);

        if (background4.transform.position.x < -32) background4.transform.position = new Vector3(39,background4.transform.position.y,0);
        if (background4_1.transform.position.x < -32) background4_1.transform.position = new Vector3(39, background4_1.transform.position.y, 0);
    }

}
