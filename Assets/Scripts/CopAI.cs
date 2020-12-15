using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopAI : MonoBehaviour
{

    //variables for movement
    string lane;
    private bool moving;
    private Transform cop;
    private Vector3 direction;
    private System.Random rand;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");

        cop = GetComponent<Transform>();
        moving = false;

        // correct for position generated in controller
        // controller generates based on a continuous distribution, but we need discrete

        float ylevel = cop.position.y;

        if (ylevel >= 3) {
            cop.position = new Vector3(cop.position.x, 4.0f, 0.0f);
            lane = "top";
        } else if (ylevel >= -3) {
            cop.position = new Vector3(cop.position.x, 0.0f, 0.0f);
            lane = "mid";
        } else {
            cop.position = new Vector3(cop.position.x, -4.0f, 0.0f);
            lane = "bot";
        }

        // change lane every 2 seconds
        InvokeRepeating("changeLane", 2, 2);
    }

    // Update is called once per frame
    void Update()
    {

        // handle movement

        switch(lane)
        {
            case "top":
                if (Vector3.Distance(cop.position, new Vector3(cop.position.x, 4.0f, 0.0f)) > 0.05f)
                {
                    cop.position = Vector3.MoveTowards(cop.position, new Vector3(cop.position.x, 4.0f, 0.0f), 4.5f * Time.deltaTime);
                }
                break;

            case "mid":
                if (Vector3.Distance(cop.position, new Vector3(cop.position.x, 0.0f, 0.0f)) > 0.05f)
                {
                    cop.position = Vector3.MoveTowards(cop.position, new Vector3(cop.position.x, 0.0f, 0.0f), 4.5f * Time.deltaTime);
                }
                break;

            case "bot":
                if (Vector3.Distance(cop.position, new Vector3(cop.position.x, -4.0f, 0.0f)) > 0.05f)
                {
                    cop.position = Vector3.MoveTowards(cop.position, new Vector3(cop.position.x, -4.0f, 0.0f), 4.5f * Time.deltaTime);
                }
                break;
        }

        // destroy when player passes the cop
        if (cop.position.x < player.transform.position.x - 2.5) {
            Destroy(gameObject);
        }
    }

    void changeLane()
    {
        print("change method called");

        bool tooClose = (cop.position.x <= player.transform.position.x + 2);


        // dont change lane if already moving
        // AND dont change lane if too close to the player (could be undodgeable)
        if (!moving && !tooClose) {
            print("SWITCHING LANE");
            switch (Random.Range(1, 4))
            {
                case 1:
                    lane = "top";
                    break;
                case 2:
                    lane = "mid";
                    break;
                case 3:
                    lane = "bot";
                    break;
            }
            print(lane);
        }
    }

}
