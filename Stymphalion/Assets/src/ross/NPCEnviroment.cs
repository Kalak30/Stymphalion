using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ss
public class NPCEnviroment : EnvirmentObjectSuperClass
{
    // Dynamic Binding lets goo
    public override void InteractFunc()
    {
        //Debug.Log("Hello");
        standeredNPC.TouchingInteractable();
    }

    private NPC standeredNPC;

    // Start is called before the first frame update
    private void Start()
    {
        standeredNPC = gameObject.AddComponent<QuestNPC>() as NPC;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}