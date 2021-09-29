using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SubClass : EnvirmentObjectSuperClass
{
   // private testScript NPC;
    public override void interactFunc(){
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
