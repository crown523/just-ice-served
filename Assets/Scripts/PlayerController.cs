using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //variables for movement
    private string lane;
    private bool moving;
    private Transform player;
    private Vector3 direction;
    public float switchSpeed = 6f;

    //variables for snowball tossing
    public float throwDelay = 1.0f;
    private float nextThrow = 0.0f;
    public Transform throwLocation;
    public GameObject snowball;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        player.position = new Vector3(-5.0f, 0, 0.0f);
        lane = "mid";

        moving = false;
        
        direction = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!EndlessGameController.isTransitioning) {
            // prevent movement during transition phase, could fix bug 
            if (Input.GetKeyDown("space") && !moving) 
            {

                moving = true;

            }
        }
        
        //throw a snowball
        if(Input.GetKeyDown("z") && Time.time > nextThrow)
        {
            nextThrow = Time.time + throwDelay;
            Instantiate(snowball, throwLocation.position, throwLocation.rotation);
        }

        if(moving)
        {
            //depending on the lane, move the player to the next one in a specified direction.
            switch (lane)
            {
                case "top":

                    direction = Vector3.down;
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) > 0.075f)
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else
                    {
                        moving = false;
                        lane = "mid";
                    }

                    break;

                case "mid":

                    if ((direction == Vector3.up) && Vector3.Distance(player.position, new Vector3(player.position.x, 4.0f, 0.0f)) > 0.075f)
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else if ((direction == Vector3.down) && Vector3.Distance(player.position, new Vector3(player.position.x, -4.0f, 0.0f)) > 0.075f)
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else
                    {

                        moving = false;
                        if (direction == Vector3.down)
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
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) > 0.075f)
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else
                    {

                        moving = false;
                        lane = "mid";
                    }

                    break;

            }

            print(lane);
        }

    }

    void FixedUpdate()
    {
        if(moving) 
        {
            switch (lane)
            {
                case "top":
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) < 0.075f)
                    {
                        moving = false;
                        lane = "mid";
                    }
                    break;

                case "mid":

                    if ((direction == Vector3.up) && Vector3.Distance(player.position, new Vector3(player.position.x, 4.0f, 0.0f)) < 0.075f
                    || ((direction == Vector3.down) && Vector3.Distance(player.position, new Vector3(player.position.x, -4.0f, 0.0f)) < 0.075f))
                    {
                        moving = false;
                        if (direction == Vector3.down)
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
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) < 0.075f)
                    {
                        moving = false;
                        lane = "mid";
                    }
                    break;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // die if contact with cop or taser
        if(other.GetComponent<CopAI>() || other.GetComponent<TaserScript>()) 
        {
            //Move to the Death Screen
            SceneManager.LoadScene("DeathScreen");
        }
    }

    public string GetLane()
    {
        return lane;
    }

}
