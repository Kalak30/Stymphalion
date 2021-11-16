using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListBuilder
{
    private List<Quest> m_quests;

    /*    public void AddToManager()
        {
            foreach (Quest q in m_quests)
            {
                QuestManager.GetQuestManager().AddQuest(q);
            }
        }*/

    public QuestListBuilder()
    {
        m_quests = new List<Quest>();
    }

    public void BuildQuests(QuestsListData quest_list_data)
    {
        foreach (QuestData q in quest_list_data.m_quests)
        {
            Quest quest_object = BuildQuest(q);
            m_quests.Add(quest_object);
        }
    }

    public List<Quest> ToQuests()
    {
        return m_quests;
    }

    private Quest BuildQuest(QuestData q)
    {
        Quest quest = new Quest(q.m_quest_name, q.m_quest_description, q.m_quest_status, q.m_quest_reward);
        quest.m_steps = BuildSteps(q);

        return quest;
    }

    private List<QuestStep> BuildSteps(QuestData quest_data)
    {
        List<QuestStep> steps = new List<QuestStep>();
        foreach (StepData s in quest_data.m_steps)
        {
            steps.Add(new QuestStep(s.m_step_name, s.m_step_description));
        }

        return steps;
    }
}