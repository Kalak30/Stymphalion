/*
*
* Filename: SubClass.cs
* Developer: Ross Prestwich
* Purpose: For testing ---- Subclass of EnvirmentObjectSuperClass.cs
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SubClass : EnvirmentObjectSuperClass
{

/// <summary>
/// Implements InteractFunc
/// Mostly For testing
/// </summary>
    public override void InteractFunc(){
        Debug.Log("Hello");
       // NPC.teest();
    }


    // Start is called before the first frame update
    void Start()
    {
     //   NPC = gameObject.AddComponent<testScript>() as testScript;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
