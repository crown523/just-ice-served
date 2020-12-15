using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


        if (moving)
        {
            switch (lane)
            {
                case "top":
                    
                    direction = Vector3.down;
                    //print(Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) > 0.05f);
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, 0.0f, 0.0f)) > 0.05f)
                    {
                        player.Translate(6f * direction * Time.deltaTime);
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
                        player.Translate(6f * direction * Time.deltaTime);
                    }
                    else if(Vector3.Distance(player.position, new Vector3(player.position.x, -4.0f, 0.0f)) > 0.05f && (direction == Vector3.down))
                    {
                        player.Translate(6f * direction * Time.deltaTime);
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
                        player.Translate(6f * direction * Time.deltaTime);
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


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<CopAI>() != null)
        {
            print("ded");
            //Application.Quit();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
