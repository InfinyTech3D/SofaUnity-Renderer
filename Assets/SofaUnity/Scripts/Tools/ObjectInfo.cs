﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SofaUnity;


public class ObjectInfo : MonoBehaviour
{
    public GameObject textUI;
    public bool displayFPS = false;

    int frameCount = 0;
    float nextUpdate = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.

    // Use this for initialization
    void Start () {
        nextUpdate = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        Text txt = textUI.GetComponent<Text>();
        SBaseMesh baseMesh = this.GetComponent<SBaseMesh>();

        int nbV = baseMesh.nbVertices();
        int nbTri = baseMesh.nbTriangles();

        if (displayFPS)
        {
            frameCount++;
            if (Time.time > nextUpdate)
            {
                nextUpdate += 1.0f / updateRate;
                fps = frameCount * updateRate;
                frameCount = 0;
            }
        }

        txt.text = "<b>Model: </b>" + baseMesh.name + "\n" +
            "<b>Vertices: </b>" + nbV + "\n" +
            "<b>Elements: </b>" + nbTri + "\n";

        if (displayFPS)
            txt.text = txt.text + "\n<b>FPS: </b>" + fps;           
        
    }
}
