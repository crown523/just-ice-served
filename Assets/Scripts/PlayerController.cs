using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // for game over FIX LATER THIS IS NOT GOOD
    public GameObject gameOverPanel;
    public GameObject endGameText;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        player.position = new Vector3(-5.0f, 0, 0.0f);
        lane = "top";

        moving = false;
        

        direction = Vector3.down;

        gameOverPanel.SetActive(false);
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
        // die if contact with cop or taser
        if(other.GetComponent<CopAI>() != null || other.GetComponent<TaserScript>() != null) 
        {
            print("ded");

            // this seems like a bad way to do it. figure out a fix later
            // in terms of encapsulation this stuff should DEFINITELY be handled by game controller
            // unfortunately im too braindead to figure it out atm
            Time.timeScale = 0;
            endGameText.GetComponent<Text>().text = "You got got. You managed to nab " + ScoreScript.score + " criminals.";
            gameOverPanel.SetActive(true);
        }
    }

}
