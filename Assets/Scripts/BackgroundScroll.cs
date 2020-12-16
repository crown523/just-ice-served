using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed = 0f;
    private MeshRenderer mesh;

    void Start()
    {
        //get the background's texture
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //make the background continually scroll
        //scroll speed depends on speed variable
        mesh.material.mainTextureOffset = new Vector2(Time.time * speed, 0f);
    }
}
