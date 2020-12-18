using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static int score = 0;
    public static int totalEnemies = 0;
    public static bool bossBeat = false;
    Text scoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        scoreCounter = GetComponent<Text>();
        if(SceneManager.GetActiveScene().name.Equals("StoryMode"))
        {
            totalEnemies = GameObject.FindObjectsOfType(typeof(EnemyAI)).Length;
        }

    }

    // Update is called once per frame
    void Update()
    {
        scoreCounter.text = "Criminals served: " + score;
       
    }

}
