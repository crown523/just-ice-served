using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndlessGameController : MonoBehaviour
{

    //gameObjects to move or create
    public GameObject player;
    public GameObject criminal;
    public GameObject cop;
    public GameObject background;

    public float scale = 2.0f;

    //used for time-based scaling
    public float speedLimit;

    //used for score-based scaling
    private bool speed1 = false;
    private bool speed2 = false;
    private bool speed3 = false;
    private bool speed4 = false;
    private bool speed5 = false;

    // ui refs
    
    private Text notifText;

    // other state vars
    
    public static bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        CancelInvoke(); // clear any leftover invokes
        notifText = GameObject.Find("notifText").GetComponent<Text>();
        
        //scale = 2f;
        ScoreScript.score = 0; // score needs to reset on restart
        if (!DeathUIManager.replayWasClicked) 
        {
            StartCoroutine(GameStartCutscene());
        }
        else
        {
            // spawn new criminals once every 2 seconds
            InvokeRepeating("SpawnCriminal", 2, 2);
            // spawn new cop every 4 seconds
            InvokeRepeating("SpawnCop", 4, 4);
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        // should constantly run to the right, camera moves with
        // should speed up as the game goes on

        //speed based on timescale. Kept commented for possible later use
        /*
        if(scale < speedLimit)
        {
            scale = (float) (2 * Math.Max(Math.Log(Time.timeSinceLevelLoad), 1));
        }
        */

        //Old-fashioned score milestone based speed up system
        //goes up to five speed tiers

        if(ScoreScript.score >= 50 && !speed5)
        {
            speed5 = true;
            scale *= 1.5f;
            CauseSpeedTransition();
        }
        else if (ScoreScript.score >= 40 && !speed4)
        {
            speed4 = true;
            scale *= 1.5f;
            CauseSpeedTransition();
        }
        else if (ScoreScript.score >= 30 && !speed3)
        {
            speed3 = true;
            scale *= 1.5f;
            CauseSpeedTransition();
        }
        else if (ScoreScript.score >= 20 && !speed2)
        {
            speed2 = true;
            scale *= 1.5f;
            CauseSpeedTransition();
        }
        else if (ScoreScript.score >= 10 && !speed1)
        {
            speed1 = true;
            scale *= 1.5f;
            CauseSpeedTransition();
        }

        player.transform.Translate(scale * Vector3.right * Time.deltaTime);
        Camera.main.transform.Translate(scale * Vector3.right * Time.deltaTime);
        background.transform.Translate(scale * Vector3.right * Time.deltaTime);
        // print(scale);

    }

    

    void SpawnCriminal() 
    {
       Instantiate(criminal, GenerateRandomPosition(12), Quaternion.identity);
    }

    void SpawnCop() 
    {
        Instantiate(cop, GenerateRandomPosition(14), Quaternion.identity);
    }

    Vector3 GenerateRandomPosition(int offset) 
    {
        // picks a random y coord (should be within the range that snowball can possibly reach) 
        // and spawns {offset} units away from the players current x
        // criminals spawn closer, cops spawn further away
        return new Vector3(player.transform.position.x + offset, UnityEngine.Random.Range(-4.5f, 4.5f), 0);
    }


    // instructions "cutscene" at start of game
    IEnumerator GameStartCutscene()
    {

        StartCoroutine(CreateNotif("Welcome to endless mode.", 3));
        yield return new WaitForSeconds(3);
        StartCoroutine(CreateNotif("Press space to move to an adjacent lane. You'll start off moving down, and change directions when in the top or bottom lane.", 4));
        yield return new WaitForSeconds(5);
        Instantiate(criminal, new Vector3(player.transform.position.x + 12, 0, 0), Quaternion.identity);
        StartCoroutine(CreateNotif("This is a criminal. Press 'z' to throw a snowball and hit criminals to gain points.", 3));
        yield return new WaitForSeconds(3);
        Instantiate(cop, new Vector3(player.transform.position.x + 14, 0, 0), Quaternion.identity);
        StartCoroutine(CreateNotif("This is a cop. Cops will change lanes and occasionally shoot out tasers.", 3));
        yield return new WaitForSeconds(3);
        StartCoroutine(CreateNotif("Hitting a cop with a snowball loses you points, and running into a cop or their taser ends the game.", 3));
        yield return new WaitForSeconds(3);
        StartCoroutine(CreateNotif("Good luck! Aim for as high a score as you can!", 3));

        // start the game proper

        yield return new WaitForSeconds(3);

        // spawn new criminals once every 2 seconds
        InvokeRepeating("SpawnCriminal", 2, 2);
        // spawn new cop every 4 seconds
        InvokeRepeating("SpawnCop", 4, 4);
        ScoreScript.score = 0;
    }

    // i want to add a "break" when the game speeds up, despawn all enemies, show a info message
    // so that the speed change isnt as abrupt
    void CauseSpeedTransition() 
    {
        isTransitioning = true;
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject gameObj in allGameObjects) {
            print(gameObj);
            // destory all criminals and cops on screen
            if (gameObj.GetComponent<EnemyAI>() || gameObj.GetComponent<CopAI>()) {
                Destroy(gameObj);
            }
        }

        // pause and restart spawning after 5 seconds
        CancelInvoke();
        InvokeRepeating("SpawnCriminal", 5, 2);
        InvokeRepeating("SpawnCop", 7, 4);
        string msg = "You've reached " + ScoreScript.score + " points! Speeding up the game. Prepare yourself!";
        StartCoroutine(CreateNotif(msg, 4));
        isTransitioning = false;
    }

    IEnumerator CreateNotif(string msg, int time) {
        notifText.text = msg;
        yield return new WaitForSeconds(time);
        notifText.text = "";
    }

}
