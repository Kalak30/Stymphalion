/*
 * Filename: QuestManager.cs
 * Developer: Jon Kopf
 * Purpose: Manage all aspects of the list of quests. Saving, loading, updating, displaying
 */

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the list of all quests within the game. This includes adding quests, removing quests,
/// and displaying quests
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_quest_manager</item>
///     <item>m_quests</item>
/// </list>
/// </summary>
public class QuestManager
{
    // Using the Singleton Pattern
    private static QuestManager m_quest_manager;

    private List<Quest> m_quests;

    private List<GameObject> m_quest_triggers;

    /// <summary>
    /// Initialize <see cref="m_quests"/> and make sure the ui is not shown
    /// </summary>
    private QuestManager()
    {
        //  GameObject.Find("QuestUI").SetActive(false);

        m_quests = new List<Quest>();
    }

    /// <summary>
    /// Gets the static <see cref="QuestManager"/> object. This implements the singleton pattern.
    /// </summary>
    /// <returns></returns>
    public static QuestManager GetQuestManager()
    {
        if (m_quest_manager is null)
        {
            m_quest_manager = new QuestManager();
            m_quest_manager.Load();
        }

        return m_quest_manager;
    }

    /// <summary>
    /// Goes to the next phase of the quest
    /// </summary>
    /// <param name="quest"></param>
    public void Next(Quest quest)
    {
        if(quest.m_quest_status == QuestStatus.locked)
        {
            quest.m_quest_status = QuestStatus.active;
            return;
        }

        quest.NextStep();
    }

    /// <summary>
    /// Add a quest object to <see cref="m_quests"/>
    /// </summary>
    /// <param name="quest"></param>
    public void AddQuest(Quest quest)
    {

        m_quests.Add(quest);
    }

    /// <summary>
    /// Display all quests within the managers list.
    /// </summary>
    public void AddToUI()
    {
        foreach (Quest quest in m_quests)
        {
            if (quest.m_quest_status == QuestStatus.active)
            {
                quest.DisplayQuest();
            }
        }
    }

    /// <param name="position"></param>
    /// <returns>Quest at position or null if position is out of bounds.</returns>
    public Quest GetQuest(int position)
    {
        // Bounds check on position
        if (position >= m_quests.Count || position < 0)
        {
            return null;
        }

        return m_quests[position];
    }

    /// <summary>
    /// Build new quests from loaded json data
    /// </summary>
    public void Load()
    {
        QuestListBuilder quest_builder = new QuestListBuilder();
        QuestListReader reader = new QuestListReader(quest_builder);
        reader.Construct("quest_file");
        m_quests = quest_builder.ToQuests();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>Length of the <see cref="m_quests"/> List</returns>
    public int QuestsLength()
    {
        return m_quests.Count;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns><see cref="QuestsListData"/> object containing serialized information about all known quests.</returns>
    public QuestsListData ToSaveData()
    {
        List<QuestData> quest_data_list = new List<QuestData>();
        foreach (Quest quest in m_quests)
        {
            quest_data_list.Add(quest.ToSaveData());
        }

        QuestsListData save_data = new QuestsListData();
        save_data.m_quests = quest_data_list;

        return save_data;
    }

    /// <summary>
    /// Affect different quest based on different triggers. This is absolutely the wrong way to do this, but I don't
    /// Have time to do it a better way
    /// </summary>
    /// <param name="collider"></param>
    public void Trigger(Collider2D collider)
    {
       
        if(collider.gameObject.name == "CaveQuestTrigger")
        {
            CaveTrigger();
        }

        if (collider.gameObject.name == "TownQuestTrigger")
        {
            TownTrigger();
        }

        if (collider.gameObject.name == "MountainQuestTrigger")
        {
            MountainTrigger();
        }
    }

    /// <summary>
    /// go to the next step in the mountain quest
    /// </summary>
    private void MountainTrigger()
    {
        Quest stym_quest = m_quests[2];

        if (stym_quest.m_active_step_pos == 0)
        {
            stym_quest.NextStep();
        }
    }

    /// <summary>
    /// Go to the next step in the town quest
    /// </summary>
    private void TownTrigger()
    {
        Quest town_quest = m_quests[0];

        if (town_quest.m_active_step_pos == 0)
        {
            town_quest.NextStep();
        }
    }

    /// <summary>
    /// Go to the next step in the cave quest
    /// </summary>
    private void CaveTrigger()
    {
        Quest hydra_quest = m_quests[1];
        
        if(hydra_quest.m_active_step_pos == 0)
        {
            hydra_quest.NextStep(); 
        }
    }
}