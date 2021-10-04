using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    private struct Quest_List : IEnumerable<Quest>
    {
        public List<Quest> quests;

        public Quest_List(Quest first)
        {
            quests = new List<Quest>();
            quests.Add(first);
        }

        public void AddQuest(Quest quest)
        {
            quests.Add(quest);
        }

        public bool RemoveQuest(Quest quest)
        {
            return quests.Remove(quest);
        }

        public IEnumerator<Quest> GetEnumerator()
        {
            return quests.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    private Quest_List quests;

    public Quest_Manager()
    {
        quests = new Quest_List();
    }

    public void DisplayQuests()
    {
        foreach (Quest quest in quests)
        {
            quest.DisplayQuest();
        }
    }
}