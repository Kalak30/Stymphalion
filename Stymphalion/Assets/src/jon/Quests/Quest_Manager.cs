using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    private Dictionary<int, Quest> quests;

    public Quest_Manager()
    {
        quests = new Dictionary<int, Quest>();
    }

    public void AddQuest(int position, Quest quest)
    {
        quests.Add(position, quest);
    }

    public Quest GetQuest(int position)
    {
        return quests[position];
    }

    public void AddQuest(int position, string quest_name, string quest_description, string quest_reward)
    {
        Quest q = new Quest(quest_name, quest_description, quest_reward);
        quests.Add(position, q);
    }

    public void DisplayQuests()
    {
        foreach (Quest quest in quests.Values)
        {
            quest.DisplayQuest();
        }
    }
}