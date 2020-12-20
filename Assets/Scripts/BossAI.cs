using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{

    //movement related code
    private string lane;
    private GameObject player;
    private Transform boss;
    private Vector3 direction;

    //snowball related code
    public Transform throwLocation;
    public GameObject bossSnowball;

    public float followTiming = 1f;
    public static int HP = 150;

    //animation related code
    public Animator anim;    
    private bool cutsceneFin;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        boss = GetComponent<Transform>();

        cutsceneFin = false;

        boss.position = new Vector3(boss.position.x, -2.5f, 0.0f);
        lane = "mid";
        anim.SetBool("cutscene", true);
        anim.SetBool("moving", false);
        StartCoroutine(CutsceneDelay());
    }

    // Update is called once per frame
    void Update()
    {
        // bring the boss on screen
        if(boss.position.x - player.transform.position.x > 12f)
        {
            
            boss.Translate(4.5f * Vector3.left * Time.deltaTime);
            anim.SetBool("moving", true);
        }
        else if(!cutsceneFin)
        {
            anim.SetBool("moving", false);
        }
        //bossfight time
        else
        {
            switch (lane)
            {
                case "top":
                    if (Vector3.Distance(boss.position, new Vector3(boss.position.x, -0.5f, 0.0f)) > 0.075f)
                    {
                        boss.position = Vector3.MoveTowards(boss.position, new Vector3(boss.position.x, -0.5f, 0.0f), 5.5f * Time.deltaTime);
                        anim.SetBool("moving", true);
                    }
                    else
                    {
                        anim.SetBool("moving", false);
                    }
                    break;

                case "mid":
                    if (Vector3.Distance(boss.position, new Vector3(boss.position.x, -2.5f, 0.0f)) > 0.075f)
                    {
                        boss.position = Vector3.MoveTowards(boss.position, new Vector3(boss.position.x, -2.5f, 0.0f), 5.5f * Time.deltaTime);
                        anim.SetBool("moving", true);
                    }
                    else
                    {
                        anim.SetBool("moving", false);
                    }
                    break;

                case "bot":
                    if (Vector3.Distance(boss.position, new Vector3(boss.position.x, -4.5f, 0.0f)) > 0.075f)
                    {
                        boss.position = Vector3.MoveTowards(boss.position, new Vector3(boss.position.x, -4.5f, 0.0f), 5.5f * Time.deltaTime);
                        anim.SetBool("moving", true);
                    }
                    else
                    {
                        anim.SetBool("moving", false);
                    }
                    break;
            }
        }
        

        //Once you beat the boss, transition to the end screen
        if(HP <= 0)
        {
            StoryScorebar.bossBeat = true;
            SceneManager.LoadScene("StoryEndScreen");
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
        anim.Play("boss-throw");
        Instantiate(bossSnowball, throwLocation.position, throwLocation.rotation);
    }

    IEnumerator CutsceneDelay()
    {

        yield return new WaitForSeconds(8);

        anim.SetBool("cutscene", false);
        cutsceneFin = true;
        // change lane in intervals
        InvokeRepeating("FollowPlayer", followTiming, followTiming);

        // Throw a snowball every so often
        InvokeRepeating("ThrowSnowball", 1, 1);
    }
}
