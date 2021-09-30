using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnviroment : EnvirmentObjectSuperClass
{
    private NPC standeredNPC; 

    public override void interactFunc(){
        //Debug.Log("Hello");
        standeredNPC.touchingInteractable();
    }

    // Start is called before the first frame update
    void Start()
    {
        standeredNPC = gameObject.AddComponent<NPC>() as NPC;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
