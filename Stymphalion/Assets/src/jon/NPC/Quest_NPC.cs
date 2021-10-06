using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_NPC : NPC
{
    private Quest npc_quest;

    public override void interactFunc()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    private void Start()
    {
        npc_quest = new Quest("start", "the first quest", "sword");
    }

    // Update is called once per frame
    private void Update()
    {
    }
}