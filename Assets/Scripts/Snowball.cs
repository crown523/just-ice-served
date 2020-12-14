using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{

    public float speed = 5.0f;
    private Rigidbody2D snowball;

    // Start is called before the first frame update
    void Start()
    {
        snowball = GetComponent<Rigidbody2D>();
        snowball.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.GetComponent<Collider2D>().gameObject);
        Destroy(gameObject);
    }
}
