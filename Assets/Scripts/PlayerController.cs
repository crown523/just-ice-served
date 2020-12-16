using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        lane = "top";

        moving = false;
        

        direction = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {

        // handle lane changing

        if (Input.GetKey("space") && !moving) 
        {

            moving = true;

            print("moving");
            //player.Translate(2 * Vector3.down * Time.deltaTime);
        }

        //check if the palyer is moving
        if (moving)
        {
            //depending on the lane, move the player to the next one in a specified direction.
            switch (lane)
            {
                case "top":
                    
                    direction = Vector3.down;
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) > 0.05f)
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

                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 4.0f, 0.0f)) > 0.05f && (direction == Vector3.up))
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else if(Vector3.Distance(player.position, new Vector3(player.position.x, -4.0f, 0.0f)) > 0.05f && (direction == Vector3.down))
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else
                    {
                        
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
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else
                    {
                        
                        moving = false;
                        lane = "mid";
                    }

                    break;
                    
            }
        }

        //throw a snowball
        if(Input.GetKey("z") && Time.time > nextThrow)
        {
            nextThrow = Time.time + throwDelay;
            Instantiate(snowball, throwLocation.position, throwLocation.rotation);
        }

    }

    //Collision with a cop
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<CopAI>() != null)
        {
            print("ded");
            
            //Application.Quit();
            ScoreScript.score = 0; // score needs to reset on death
            //Reloads the scene on death
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
