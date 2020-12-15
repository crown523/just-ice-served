using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject criminal;

    public GameObject cop;


    // Start is called before the first frame update
    void Start()
    {
        // spawn new criminals once every 2 seconds
        InvokeRepeating("SpawnCriminal", 2, 2);
        // spawn new cop every 4 seconds
        InvokeRepeating("SpawnCop", 4, 4);
    }

    // Update is called once per frame
    void Update()
    {
        // should constantly run to the right, camera moves with
        // should speed up as the game goes on
        float scale = (float) Math.Max(Math.Log(Time.timeSinceLevelLoad), 1);
        Player.transform.Translate(scale * Vector3.right * Time.deltaTime);
        Camera.main.transform.Translate(scale * Vector3.right * Time.deltaTime);
        
    }

    void SpawnCriminal() 
    {
       Instantiate(criminal, GenerateRandomPosition(12), new Quaternion(0,0,0,0));
    }

    void SpawnCop() 
    {
        Instantiate(cop, GenerateRandomPosition(14), new Quaternion(0,0,0,0));
    }

    Vector3 GenerateRandomPosition(int offset) 
    {
        // picks a random y coord (should be within the range that snowball can possibly reach) 
        // and spawns {offset} units away from the players current x
        // criminals spawn closer, cops spawn further away
        return new Vector3(Player.transform.position.x + offset, UnityEngine.Random.Range(-4.5f, 4.5f), 0);
    }
}
