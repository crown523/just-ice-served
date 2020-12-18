using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private string lane;
    

    private GameObject player;
    private Transform boss;
    private Vector3 direction;


    private Transform throwLocation;
    public GameObject snowball;

    public float followTiming = 0.5f;
    public int HP;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        boss = GetComponent<Transform>();

        boss.position = new Vector3(boss.position.x, 0.0f, 0.0f);
        lane = "mid";

        // change lane every 2 seconds
        InvokeRepeating("FollowPlayer", followTiming, followTiming);

        // since taser lasts 2 seconds, this casts taser every 2 seconds
        //InvokeRepeating("ThrowSnowball", 4, 6);
    }

    // Update is called once per frame
    void Update()
    {
        // handle movement

        switch (lane)
        {
            case "top":
                if (Vector3.Distance(boss.position, new Vector3(boss.position.x, 4.0f, 0.0f)) > 0.075f)
                {
                    boss.position = Vector3.MoveTowards(boss.position, new Vector3(boss.position.x, 4.0f, 0.0f), 5.5f * Time.deltaTime);
                }
                break;

            case "mid":
                if (Vector3.Distance(boss.position, new Vector3(boss.position.x, 0.0f, 0.0f)) > 0.075f)
                {
                    boss.position = Vector3.MoveTowards(boss.position, new Vector3(boss.position.x, 0.0f, 0.0f), 5.5f * Time.deltaTime);
                }
                break;

            case "bot":
                if (Vector3.Distance(boss.position, new Vector3(boss.position.x, -4.0f, 0.0f)) > 0.075f)
                {
                    boss.position = Vector3.MoveTowards(boss.position, new Vector3(boss.position.x, -4.0f, 0.0f), 5.5f * Time.deltaTime);
                }
                break;
        }
    }

    void FollowPlayer()
    {
        string playerLane = player.GetComponent<PlayerController>().GetLane();
       

        if(!lane.Equals(playerLane))
        {
            lane = playerLane;
            print(lane);
        }
    }

    void ThrowSnowball()
    {
        Instantiate(snowball, throwLocation.position, throwLocation.rotation);
    }
}
