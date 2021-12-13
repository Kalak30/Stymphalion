/*
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

//Singleton Pattern
public class DemoMode : MonoBehaviour
{

    private static DemoMode _instance;

    protected DemoMode() { }

    public static DemoMode Instance { get { return _instance; } }

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
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Keyboard.current.FindKeyOnCurrentKeyboardLayout("space");
            Debug.Log("Swag");
        }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    /// <summary>
    /// Main function. Checks if player has made any key inputs for 10 seconds, and if not, loads
    /// 1 of 2 replays (failure and success), then plays the replay. If the player makes any key input, 
    /// stop the replay.
    /// </summary>
    void FixedUpdate()
    {
        

        if(Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && player_Inputted == 0)
        {
            player_Inputted = 1;
        }
        
        if (!Input.anyKey && time < idle_Time & player_Inputted == 0)
        {
            time = time + 1;
        }

        if(time == idle_Time & in_Demo == 0)
        {
            in_Demo = 1;
            string path = "Assets/Resources/InputRecorderTest.inputtrace";
            string path2 = "Assets/Resources/InputRecorder2.inputtrace";
            int random_Int = Random.Range(1, 3);
            Debug.Log("Playing path "+random_Int);

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

        if(Input.anyKey && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && in_Demo > 0)
        {
            testClass.PauseReplay();
            testClass.ClearCapture();
        }

        
    }

    private void InputSystem_onEvent(InputEventPtr arg1, InputDevice arg2)
    {
        throw new System.NotImplementedException();
    }
}
