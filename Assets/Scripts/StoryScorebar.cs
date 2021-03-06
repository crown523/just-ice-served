﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScorebar : MonoBehaviour
{
    public static int totalEnemies = 25;

    public static int enemiesBeat = 0;
    public static bool bossBeat = false;
    

    private Slider scoreBar;
    private Text barLabel;

    // Start is called before the first frame update
    void Start()
    {
        scoreBar = GetComponent<Slider>();
        barLabel = GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        print(enemiesBeat);
        // this should be done via an event, but idk how to use those really
        if (enemiesBeat < totalEnemies)
        {
            barLabel.text = "Progress";
            if (scoreBar.value < (float) enemiesBeat / totalEnemies)
            {
                scoreBar.value += 0.0043f; // creates gradual fill effect (although its pretty fast lmao)
            }
        }
        else if (enemiesBeat == totalEnemies)
        {
            scoreBar.value = 1;
            enemiesBeat++; // very hacky. i think it works just trust the process :)
        }
        else
        {
            barLabel.text = "Boss HP";
            if (scoreBar.value > (float) BossAI.HP / 150)
            {
                scoreBar.value -= 0.0043f;
            }
        }
        
        
    }
}
