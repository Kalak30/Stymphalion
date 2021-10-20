/*
*
* Filename: NPCEviroment.cs
* Developer: Ross Prestwich
* Purpose: For Interacting with NPCs ---- Subclass of EnvirmentObjectSuperClass.cs
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ss
public class NPCEnviroment : EnvirmentObjectSuperClass
{
    private NPC m_standered_NPC; 

    // Dynamic Binding lets goo
    public override void InteractFunc(){
        //Debug.Log("Hello");
        m_standered_NPC.touchingInteractable();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_standered_NPC = gameObject.AddComponent<Quest_NPC>() as NPC;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
