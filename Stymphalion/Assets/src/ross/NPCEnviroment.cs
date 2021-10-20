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

/// <summary>
/// Implements Interact function for friendly NPC's
/// </summary>
    public override void InteractFunc(){
        //Debug.Log("Hello");
        m_standered_NPC.touchingInteractable();
    }

/// <summary>
/// Get Quest_NPC class
/// </summary>
    void Start()
    {
        m_standered_NPC = gameObject.AddComponent<Quest_NPC>() as NPC;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
