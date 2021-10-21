/*
 * Filename: QuestManager.cs
 * Developer: Jon Kopf
 * Purpose:
 */

using System.Collections.Generic;

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

    /// <summary>
    /// Initialize <see cref="m_quests"/>
    /// </summary>
    private QuestManager()
    {
        m_quests = new List<Quest>();
    }

    /// <summary>
    /// Gets the static <see cref="QuestManager"/> object. This implements the singleton pattern.
    /// </summary>
    /// <returns></returns>
    public static QuestManager GetQuest_Manager()
    {
        if (m_quest_manager is null)
        {
            m_quest_manager = new QuestManager();
        }

        return m_quest_manager;
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
    /// <list type="bullet">
    /// <item></item>
    /// </list>
    /// </summary>
    /// <param name="quest_name"></param>
    /// <param name="quest_description"></param>
    /// <param name="quest_reward"></param>
    public void AddQuest(string quest_name, string quest_description, Item quest_reward)
    {
        Quest quest = new Quest(quest_name, quest_description, quest_reward);
        m_quests.Add(quest);
    }

    /// <summary>
    /// Display all quests within the managers list.
    /// </summary>
    public void DisplayQuests()
    {
        foreach (Quest quest in m_quests)
        {
            quest.DisplayQuest();
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
    /// Updates the status of a quest once it has been completed.
    /// </summary>
    /// <param name="position">Position within <see cref="m_quests"/> list to be updated</param>
    public void TurnInQuest(int position)
    {
        Quest quest = m_quests[position];
        if (quest.GetStatus() == QuestStatus.completed)
        {
            quest.UpdateStatus(QuestStatus.finished);
            //Give player their reward
        }
    }
}