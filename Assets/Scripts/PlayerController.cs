using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables for movement
    string lane;
    private bool moving;
    private Transform player;
    private Vector3 direction;

    //variable 
    private float throwDelay = 1.0f;
    private float nextThrow = 0.0f;
    public Transform throwLocation;
    public GameObject snowball;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        player.position = new Vector3(-5.0f, 4.0f, 0.0f);
        lane = "top";

        moving = false;
        

        direction = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        //print(player.position.y);

        if (Input.GetKey("down") && !moving) 
        {

            moving = true;

            print("moving");
            //player.Translate(2 * Vector3.down * Time.deltaTime);
        }


        if (moving)
        {
            switch (lane)
            {
                case "top":
                    
                    direction = Vector3.down;
                    //print(Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) > 0.05f);
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) > 0.05f)
                    {
                        player.Translate(3f * direction * Time.deltaTime);
                    }
                    else
                    {
                        print(player.position.y);
                        moving = false;
                        lane = "mid";
                    }

                    break;
                    
                case "mid":

                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 4.0f, 0.0f)) > 0.05f && (direction == Vector3.up))
                    {
                        player.Translate(3f * direction * Time.deltaTime);
                    }
                    else if(Vector3.Distance(player.position, new Vector3(player.position.x, -4.0f, 0.0f)) > 0.05f && (direction == Vector3.down))
                    {
                        player.Translate(3f * direction * Time.deltaTime);
                    }
                    else
                    {
                        print(player.position.y);
                        moving = false;
                        if(Vector3.Distance(player.position, new Vector3(player.position.x, 4.0f, 0.0f)) > Vector3.Distance(player.position, new Vector3(player.position.x, -4.0f, 0.0f)))
                        {
                            lane = "bot";
                        }
                        else
                        {
                            lane = "top";
                        }
                        
                    }

                    break;

                case "bot":

                    direction = Vector3.up;
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) > 0.05f)
                    {
                        player.Translate(3f * direction * Time.deltaTime);
                    }
                    else
                    {
                        print(player.position.y);
                        moving = false;
                        lane = "mid";
                    }

                    break;
                    
            }
        }

        //throw a snowball
        if(Input.GetKey("space") && Time.time > nextThrow)
        {
            nextThrow = Time.time + throwDelay;
            Instantiate(snowball, throwLocation.position, throwLocation.rotation);
        }

    }
}
