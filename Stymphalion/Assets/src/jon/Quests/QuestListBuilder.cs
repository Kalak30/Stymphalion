/*
 * Filename: QuestListBuilder.cs
 * Developer: Jon Kopf
 * Purpose: Allow for quests to be created an encapsulated environment from save data
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListBuilder
{
    private List<Quest> m_quests;


    /// <summary>
    /// Initialize quest list
    /// </summary>
    public QuestListBuilder()
    {
        m_quests = new List<Quest>();
    }

    /// <summary>
    /// Builds quests from a list of quest save data
    /// </summary>
    /// <param name="quest_list_data"></param>
    public void BuildQuests(QuestsListData quest_list_data)
    {
        foreach (QuestData q in quest_list_data.m_quests)
        {
            Quest quest_object = BuildQuest(q);
            m_quests.Add(quest_object);
        }
    }

    /// <summary>
    /// Returns the list of all quests built
    /// </summary>
    /// <returns></returns>
    public List<Quest> ToQuests()
    {
        return m_quests;
    }

    /// <summary>
    /// Initializes a new quest based on quest save data
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    private Quest BuildQuest(QuestData q)
    {
        Quest quest = new Quest(q.m_quest_name, q.m_quest_description, q.m_quest_status, q.m_quest_reward);
        quest.m_steps = BuildSteps(q);

        return quest;
    }

    /// <summary>
    /// Initializes a list of steps based on saved quest data
    /// </summary>
    /// <param name="quest_data"></param>
    /// <returns></returns>
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