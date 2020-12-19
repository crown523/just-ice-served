using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static int score = 0;

    //this is set in the storygamecontroller script due to how that script functions
    public static int totalEnemies = 0;
    public static bool bossBeat = false;
    Text scoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        scoreCounter = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounter.text = "Criminals served: " + score + "/" + totalEnemies;
       
    }

}
