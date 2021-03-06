﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScroll : MonoBehaviour
{
    //check the active gamemode
    private string scene;

    public float speed = 0f;
    private MeshRenderer mesh;
    private float ScreenHeight, ScreenWidth;

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;

        //get the background's texture
        mesh = GetComponent<MeshRenderer>();

        ScreenHeight = Camera.main.orthographicSize * 2;
        ScreenWidth = ScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(ScreenWidth, ScreenHeight, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //make the background continually scroll
        //scroll speed depends on speed variable
        mesh.material.mainTextureOffset = new Vector2(Time.time * speed, 0f);

        ScreenHeight = Camera.main.orthographicSize * 2;
        ScreenWidth = ScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(ScreenWidth, ScreenHeight, 1);
    }
}
