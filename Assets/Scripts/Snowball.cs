using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private GameObject controller;
    private float speed = 5.0f;
    private Rigidbody2D snowball;
    private float createdTime;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameController");
        snowball = GetComponent<Rigidbody2D>();
        snowball.velocity = transform.right * (controller.GetComponent<GameController>().scale/2 * speed);
        createdTime = Time.time;
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
        }
        else if(other.GetComponent<CopAI>() != null)
        {
            Destroy(gameObject);
            ScoreScript.score--;
        }
        
    }
}
