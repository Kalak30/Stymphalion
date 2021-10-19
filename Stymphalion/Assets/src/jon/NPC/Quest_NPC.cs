using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_NPC : NPC
{
    public Quest npc_quest;

    public override void touchingInteractable()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        npc_quest = new Quest("start", "the first quest", "sword");
        Quest_Manager.GetQuest_Manager().AddQuest(npc_quest);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}