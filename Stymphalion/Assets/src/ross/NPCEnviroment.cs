using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ss
public class NPCEnviroment : EnvirmentObjectSuperClass
{
    public bool m_is_quest;
    public bool m_is_shop;
    private NPC standeredNPC;

    // Dynamic Binding lets goo
    public override void InteractFunc()
    {
        //Debug.Log("Hello");
        standeredNPC.TouchingInteractable();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (m_is_shop)
        {
            standeredNPC = gameObject.AddComponent<ShopNPC>() as NPC;
        }
        else if (m_is_quest)
        {
            standeredNPC = gameObject.AddComponent<QuestNPC>() as NPC;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}