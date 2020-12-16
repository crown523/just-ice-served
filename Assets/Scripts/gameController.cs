using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
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

    public Text notifText;

    

    // Start is called before the first frame update
    void Start()
    {
        // spawn new criminals once every 2 seconds
        InvokeRepeating("SpawnCriminal", 2, 2);
        // spawn new cop every 4 seconds
        InvokeRepeating("SpawnCop", 4, 4);

        //testing method for speed
        // InvokeRepeating("SpawnTest", 0, 1);

       
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

        // i want to add a "break" when the game speeds up, despawn all enemies, show a info message

        if(ScoreScript.score > 50 && !speed5)
        {
            speed5 = true;
            scale *= 2;
            CauseSpeedTransition();
        }
        else if (ScoreScript.score > 40 && !speed4)
        {
            speed4 = true;
            scale *= 2;
            CauseSpeedTransition();
        }
        else if (ScoreScript.score > 30 && !speed3)
        {
            speed3 = true;
            scale *= 2;
            CauseSpeedTransition();
        }
        else if (ScoreScript.score > 20 && !speed2)
        {
            speed2 = true;
            scale *= 2;
            CauseSpeedTransition();
        }
        else if (ScoreScript.score > 10 && !speed1)
        {
            speed1 = true;
            scale *= 2;
            CauseSpeedTransition();
        }

        player.transform.Translate(scale * Vector3.right * Time.deltaTime);
        Camera.main.transform.Translate(scale * Vector3.right * Time.deltaTime);
        background.transform.Translate(scale * Vector3.right * Time.deltaTime);
        // print(scale);
    }

    //Test method for speed scale debugging
    void SpawnTest()
    {
        Instantiate(criminal, new Vector3(player.transform.position.x + 15, player.transform.position.y, 0), Quaternion.identity);
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


    // i want to add a "break" when the game speeds up, despawn all enemies, show a info message
    // so that the speed change isnt as abrupt
    void CauseSpeedTransition() 
    {
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
        StartCoroutine(createNotif(msg, 4));
    }

    IEnumerator createNotif(string msg, int time) {
        notifText.text = msg;
        yield return new WaitForSeconds(time);
        notifText.text = "";
    }

    public void OnClickReplay()
    {
        ScoreScript.score = 0; // score needs to reset on restart
        Time.timeScale = 1; // reset time scale
        //CancelInvoke(); // seems to be necessary
        // restart the game (reload scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMainMenu()
    {
        // nothing yet
    }
    
}
