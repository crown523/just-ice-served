using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //check the scene to transition to the right game end scene
    private string scene;

    //variables for movement
    public bool controlsActive;
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

    //variables only used in story mode
    public int HP = 30; // should the player even have HP? i was thinking more bullet hell style tbh
    public bool finished = false;


    //animator variables
    public Animator anim;
    public GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;

        controlsActive = true;

        player = GetComponent<Transform>();
        player.position = new Vector3(-5.0f, -2.5f, 0.0f);
        
        lane = "mid";

        moving = false;
        
        direction = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {

        if(scene.Equals("EndlessMode"))
        {
            anim.SetFloat("scale", controller.GetComponent<EndlessGameController>().scale);
        }
        else
        {
            anim.SetFloat("scale", controller.GetComponent<StoryGameController>().scrollSpeed);
        }
        
        if (!EndlessGameController.isTransitioning) {
            // prevent movement during transition phase, could fix bug 
            if (Input.GetKeyDown("space") && !moving && controlsActive) 
            {
                
                moving = true;
                
                anim.SetBool("moving", true);
            }
        }
        
        //throw a snowball
        if(Input.GetKeyDown("z") && Time.time > nextThrow && controlsActive)
        {
            anim.Play("player-throw");
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
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, -2.5f, 0.0f)) > 0.075f)
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else
                    {
                        moving = false;
                        lane = "mid";
                        anim.SetBool("moving", false);
                    }

                    break;

                case "mid":

                    if ((direction == Vector3.up) && Vector3.Distance(player.position, new Vector3(player.position.x, -0.5f, 0.0f)) > 0.075f)
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else if ((direction == Vector3.down) && Vector3.Distance(player.position, new Vector3(player.position.x, -4.5f, 0.0f)) > 0.075f)
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
                        anim.SetBool("moving", false);

                    }

                    break;

                case "bot":

                    direction = Vector3.up;
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, -2.5f, 0.0f)) > 0.075f)
                    {
                        player.Translate(switchSpeed * direction * Time.deltaTime);
                    }
                    else
                    {

                        moving = false;
                        lane = "mid";
                        anim.SetBool("moving", false);
                    }

                    break;

            }

            //print(lane);
        }

        //if your hp drops to 0 during the bossfight
        if (scene.Equals("StoryMode") && HP <= 0)
        {
            SceneManager.LoadScene("StoryEndScreen");
        }

    }

    void FixedUpdate()
    {
        if(moving) 
        {
            switch (lane)
            {
                case "top":
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, -2.5f, 0.0f)) < 0.075f)
                    {
                        moving = false;
                        lane = "mid";
                        anim.SetBool("moving", false);
                    }
                    break;

                case "mid":

                    if ((direction == Vector3.up) && Vector3.Distance(player.position, new Vector3(player.position.x, -0.5f, 0.0f)) < 0.075f
                    || ((direction == Vector3.down) && Vector3.Distance(player.position, new Vector3(player.position.x, -4.5f, 0.0f)) < 0.075f))
                    {
                        moving = false;
                        anim.SetBool("moving", false);
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
                    if (Vector3.Distance(player.position, new Vector3(player.position.x, -2.5f, 0.0f)) < 0.075f)
                    {
                        moving = false;
                        anim.SetBool("moving", false);
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
            //Move to the Death Screen if in endless mode
            if(scene.Equals("EndlessMode"))
            {
                SceneManager.LoadScene("DeathScreen");
            }
            else
            {
                StoryEndUI.diedToCop = true;
                SceneManager.LoadScene("StoryEndScreen");
            }
            
        }
        else if(other.name.Equals("FinishLine"))
        {
            //print("done");
            finished = true;
        }
        
    }

    public string GetLane()
    {
        return lane;
    }

}
