using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CopAI : MonoBehaviour
{
    //string check for the current gamemode
    private string scene;

    //variables for movement
    public string lane;
    private bool moving;
    private Transform cop;
    private Vector3 direction;
    private GameObject player;

    //taser related variables
    //bottom 
    public GameObject taser;

    //animation variables
    public Animator anim;
    private float timeTaserCreated;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;

        player = GameObject.Find("Player");

        cop = GetComponent<Transform>();
        anim.SetBool("moving", false);
        anim.SetBool("taser", false);

        //Added a scene check to make the cops simpler to hardcode for Story Mode
        if (scene.Equals("EndlessMode"))
        {
            // correct for position generated in controller
            // controller generates based on a continuous distribution, but we need discrete

            float ylevel = cop.position.y;

            if (ylevel >= 3)
            {
                cop.position = new Vector3(cop.position.x, -0.5f, 0.0f);
                lane = "top";
            }
            else if (ylevel >= -3)
            {
                cop.position = new Vector3(cop.position.x, -2.5f, 0.0f);
                lane = "mid";
            }
            else
            {
                cop.position = new Vector3(cop.position.x, -4.5f, 0.0f);
                lane = "bot";
            }
            // change lane (endless mode) every 1.5 seconds
            InvokeRepeating("ChangeLaneEndless", 2, 1.5f);
        }
        else
        {
            switch(lane)
            {
                case "top":
                    cop.position = new Vector3(cop.position.x, -0.5f, 0.0f);
                    direction = Vector3.down;
                    break;
                case "mid":
                    cop.position = new Vector3(cop.position.x, -2.5f, 0.0f);
                    direction = Vector3.down;
                    break;
                case "bot":
                    cop.position = new Vector3(cop.position.x, -4.5f, 0.0f);
                    direction = Vector3.up;
                    break;
            }

            // change lane (story mode) every 2 seconds
            InvokeRepeating("ChangeLaneStory", 2, 1.5f);

        }

        // since taser lasts 2 seconds, this casts taser every 2 seconds
        InvokeRepeating("UseTaser", 4, 6);
    }

    // Update is called once per frame
    void Update()
    {

        // handle movement

        switch(lane)
        {
            case "top":
                if (Vector3.Distance(cop.position, new Vector3(cop.position.x, -0.5f, 0.0f)) > 0.05f)
                {
                    cop.position = Vector3.MoveTowards(cop.position, new Vector3(cop.position.x, -0.5f, 0.0f), 4.5f * Time.deltaTime);
                    anim.SetBool("moving", true);
                }
                else
                {
                    anim.SetBool("moving", false);
                }
                break;

            case "mid":
                if (Vector3.Distance(cop.position, new Vector3(cop.position.x, -2.5f, 0.0f)) > 0.05f)
                {
                    cop.position = Vector3.MoveTowards(cop.position, new Vector3(cop.position.x, -2.5f, 0.0f), 4.5f * Time.deltaTime);
                    anim.SetBool("moving", true);
                }
                else
                {
                    anim.SetBool("moving", false);
                }
                break;

            case "bot":
                if (Vector3.Distance(cop.position, new Vector3(cop.position.x, -4.5f, 0.0f)) > 0.05f)
                {
                    cop.position = Vector3.MoveTowards(cop.position, new Vector3(cop.position.x, -4.5f, 0.0f), 4.5f * Time.deltaTime);
                    anim.SetBool("moving", true);
                }
                else
                {
                    anim.SetBool("moving", false);
                }
                break;
        }

        // destroy when player passes the cop
        if (cop.position.x < player.transform.position.x - 2.5) {
            Destroy(gameObject);
        }

        //leave taser animation
        if(Time.time-timeTaserCreated >= 2f)
        {
            anim.SetBool("taser", false);
        }
    }

    //Random lane change style for endless mode
    void ChangeLaneEndless()
    {
        //print("change method called");

        bool tooClose = (cop.position.x <= player.transform.position.x + 3);

        // dont change lane if already moving
        // AND dont change lane if too close to the player (could be undodgeable)
        if (!moving && !tooClose) {
            //print("SWITCHING LANE");
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
            //print(lane);
        }
    }

    void ChangeLaneStory()
    {
        bool tooClose = (cop.position.x <= player.transform.position.x + 3);

        if(!tooClose)
        {
            if (lane.Equals("top"))
            {
                direction = Vector3.down;
                lane = "mid";
            }
            else if (lane.Equals("mid"))
            {
                if (direction.Equals(Vector3.up))
                {
                    lane = "top";
                }
                else
                {
                    lane = "bot";
                }
            }
            else
            {
                direction = Vector3.up;
                lane = "mid";
            }
        }

        
    }

    void UseTaser()
    {
        //print("taser created");

        // creates taser at the "front" of the cop
        // attached to cop as parent, so moves with it
        Instantiate(taser, new Vector3(cop.position.x - 1.25f, cop.position.y + 0.24f, cop.position.z), cop.rotation, cop);

        timeTaserCreated = Time.time;
        anim.SetBool("taser", true);

    }

}
