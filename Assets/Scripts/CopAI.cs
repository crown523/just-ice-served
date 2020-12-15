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

    // Start is called before the first frame update
    void Start()
    {
        cop = GetComponent<Transform>();
        rand = new System.Random();
        moving = false;


        switch (rand.Next(1, 4))
        {
            case 1:
                cop.position = new Vector3(5.0f, 4.0f, 0.0f);
                lane = "top";
                break;
            case 2:
                cop.position = new Vector3(5.0f, 0f, 0.0f);
                lane = "mid";
                break;
            case 3:
                cop.position = new Vector3(5.0f, -4.0f, 0.0f);
                lane = "bot";
                break;
        }

        //InvokeRepeating("changeLane", 1.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void changeLane()
    {
        switch (rand.Next(1, 4))
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
