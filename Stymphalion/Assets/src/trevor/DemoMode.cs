/*
 * 
 * Filename: DemoMode.CS 
 * Developer: Trevor McGeary
 * Purpose: Put the player character into the demo mode
 */




//using System;
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
    public int player_Inputted = 0;
    
    //public static InputEventTrace LoadFrom(string path);

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
        /*if(Input.anyKey && player_Inputted == 0)
        {
            player_Inputted = 1;
        }*/
        
        if (!Input.anyKey && time < idle_Time & player_Inputted == 0)
        {
            time = time + 1;
        }

        if(time == idle_Time & in_Demo == 0)
        {
            in_Demo++;
            string path = "Assets/Resources/InputRecorderTest.inputtrace";
            string path2 = "Assets/Resources/InputRecorder2.inputtrace";
            int random_Int = Random.Range(0, 3);
            Debug.Log(random_Int);
            //testClass.testFunction();
            //testClass.StartReplay();

            //StreamReader reader = new StreamReader(path);
            if (random_Int == 1)
            {
                testClass.LoadCaptureFromFile(path);
                testClass.StartReplay();
            }
            else if (random_Int == 2)
            {
                testClass.LoadCaptureFromFile(path2);
                testClass.StartReplay();
            }
        }

        if (Input.anyKey && in_Demo > 0)
        {
            testClass.PauseReplay();
            //testClass.LoadCaptureFromFile("Assets/Resources/InputRecorderTest.inputtrace");
            //testClass.SaveCaptureToFile("InputRecorder3.inputtrace");
            //testClass.LoadCaptureFromFile("InputRecorder3.inputtrace");
            //testClass.StartReplay();
        }

        
    }
}
