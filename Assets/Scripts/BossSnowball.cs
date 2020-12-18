using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSnowball : MonoBehaviour
{
    private GameObject controller;
    private float speed = -5.0f;
    private Rigidbody2D snowball;
    private float createdTime;

    // Start is called before the first frame update
    void Start()
    {
        snowball = GetComponent<Rigidbody2D>();
        
        createdTime = Time.time;

        controller = GameObject.Find("GameController");
        snowball.velocity = transform.right * (controller.GetComponent<StoryGameController>().snowballSpeed / 2 * speed);


    }

    void Update()
    {
        if (Time.time - createdTime >= 4)
        {
            Destroy(gameObject);
        }
    }

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            //Destroy(other.gameObject);
            Destroy(gameObject);
            other.GetComponent<>().HP -= 10;

        }

    }
}
