using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : NPC
{
    public Quest npc_quest;

    public override void TouchingInteractable()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        npc_quest = new Quest("start", "the first quest", null);
        Quest_Manager.GetQuest_Manager().AddQuest(npc_quest);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}