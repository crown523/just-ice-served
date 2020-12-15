using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // should constantly run to the right, camera moves with

        Player.transform.Translate(2 * Vector3.right * Time.deltaTime);
        Camera.main.transform.Translate(2 * Vector3.right * Time.deltaTime);
    }
}
