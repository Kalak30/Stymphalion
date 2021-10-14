using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ss
public class NPCEnviroment : EnvirmentObjectSuperClass
{
    private NPC standeredNPC; 

    // Dynamic Binding lets goo
    public override void interactFunc(){
        //Debug.Log("Hello");
        standeredNPC.touchingInteractable();
    }

    // Start is called before the first frame update
    void Start()
    {
        standeredNPC = gameObject.AddComponent<Quest_NPC>() as NPC;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
