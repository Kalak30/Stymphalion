/*
 * Filename: QuestSaveData.cs
 * Developer: Jon Kopf
 * Purpose: This file contains all data required to save information to disk.
 *          Data saved to disk needs to be serializable so they can be read and written accurately.
 */

using System.Collections.Generic;

/// <summary>
/// All data needed to characterize a particular quest
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_active_step_pos</item>
///     <item>m_quest_description</item>
///     <item>m_quest_name</item>
///     <item>m_quest_reward</item>
///     <item>m_quest_status</item>
///     <item>m_steps</item>
/// </list>
/// </summary>
[System.Serializable]
public class QuestData
{
    public int m_active_step_pos;
    public string m_quest_description;
    public string m_quest_name;
    public Item.ItemType m_quest_reward;
    public QuestStatus m_quest_status;
    public List<StepData> m_steps;
}

/// <summary>
/// A collection of serializable <see cref="QuestData"/>
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_quests</item>
/// </list>
/// </summary>
[System.Serializable]
public class QuestsListData
{
    public List<QuestData> m_quests;
}

/// <summary>
/// All data needed to characterize a step
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_step_description</item>
///     <item>m_step_name</item>
/// </list>
/// </summary>
[System.Serializable]
public class StepData
{
    public string m_step_description;
    public string m_step_name;
}