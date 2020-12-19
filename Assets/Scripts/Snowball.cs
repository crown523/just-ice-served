using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snowball : MonoBehaviour
{
    //string check for the current gamemode
    private string scene;

    private GameObject controller;
    private float speed = 5.0f;
    private Rigidbody2D snowball;
    private float createdTime;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        snowball = GetComponent<Rigidbody2D>();
        controller = GameObject.Find("GameController");
        createdTime = Time.time;

        if(scene.Equals("EndlessMode"))
        {
            snowball.velocity = transform.right * (controller.GetComponent<EndlessGameController>().scale / 2 * speed);
        }
        else
        {
            snowball.velocity = transform.right * (controller.GetComponent<StoryGameController>().snowballSpeed / 2 * speed);
        }
        
    }

    void Update()
    {
        if (Time.time - createdTime >= 4) {
            Destroy(gameObject);
        }
    }

    void OnBecomeInvisible() 
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<EnemyAI>() != null)
        {
            //Destroy(other.GetComponent<Collider2D>().gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);

            ScoreScript.score++;
            // these 2
            // should techincally be wrapped in if
            // but wtv
            StoryScorebar.enemiesBeat++;
            
            
        }
        else if(other.GetComponent<CopAI>() != null)
        {
            Destroy(gameObject);

            if (scene.Equals("EndlessMode"))
            {
                ScoreScript.score--;
            }
        }
        else if (other.GetComponent<BossAI>() != null)
        {
            Destroy(gameObject);
            BossAI.HP -= 10;
        }


    }
}
