using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager
{
    private static Quest_Manager quest_manager;

    private List<Quest> quests;

    private Quest_Manager()
    {
        quests = new List<Quest>();
    }

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
    }

    public Quest GetQuest(int position)
    {
        return quests[position];
    }

    public void AddQuest(string quest_name, string quest_description, string quest_reward)
    {
        Quest quest = new Quest(quest_name, quest_description, quest_reward);
        quests.Add(quest);
    }

    public void DisplayQuests()
    {
        foreach (Quest quest in quests)
        {
            quest.DisplayQuest();
        }
    }

    public void TurnInQuest(int position)
    {
        Quest quest = quests[position];
        if (quest.GetStatus() == Quest.Status.completed)
        {
            quest.UpdateStatus(Quest.Status.finished);
            //Give player their reward
        }
    }

    public Quests_List_Data ToSaveData()
    {
        List<Quest_Data> quest_data_list = new List<Quest_Data>();
        foreach (Quest quest in quests)
        {
            quest_data_list.Add(quest.ToSaveData());
        }

        Quests_List_Data save_data = new Quests_List_Data();
        save_data.quests = quest_data_list;

        return save_data;
    }

    public bool LoadQuests(string file_name)
    {
        return true;
        //JSON_Quest_Reader.ReadFile("quest"); ;
    }

    public static Quest_Manager GetQuest_Manager()
    {
        if (quest_manager is null)
        {
            quest_manager = new Quest_Manager();
        }

        return quest_manager;
    }
}