using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserScript : MonoBehaviour
{

    private float createdTime;

    // Start is called before the first frame update
    void Start()
    {   
        createdTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        // taser lasts 2 seconds
        if (Time.time - createdTime >= 2) {
            Destroy(gameObject);
        }
    }
}
