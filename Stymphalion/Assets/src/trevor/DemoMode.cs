/*
 * 
 * Filename: DemoMode.CS 
 * Developer: Trevor McGeary
 * Purpose: Put the player character into the demo mode
 */




using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


public class DemoMode : MonoBehaviour
{

    public InputRecorder testClass;

    public int idle_Time = 100;
    public int time = 0;
    public int in_Demo = 0;

    public string path = @"C:\Users\trevo\OneDrive\Documents\GitHub\Stymphalion\Stymphalion\InputRecorderTest";

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        if (!Input.anyKey && time < idle_Time)
        {
            time = time + 1;
        }

        if(time == idle_Time & in_Demo == 0)
        {
            in_Demo++;

            //testClass.testFunction();
            //testClass.StartReplay();
            //testClass.LoadCaptureFromFile(path);
            //testClass.StartReplay();
        }

        //if (Input.anyKey && in_Demo > 0)
        //{
         //   testClass.StopReplay();
        //}
    }
}
