/*
 * Filename: Quest.cs
 * Developer: Jon Kopf
 * Purpose: Provide the data required for a single Quest
 */

using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Status of a <see cref="Quest"/>
/// </summary>
public enum QuestStatus
{
    /// <summary>
    /// If a <see cref="Quest"/> is actively being worked on by the player.
    /// </summary>
    active = 0,

    /// <summary>
    /// If the <see cref="Quest"/> has been failed.
    /// </summary>
    failed = 1,

    /// <summary>
    /// If the <see cref="Quest"/> is locked to the player currently.
    /// </summary>
    locked = 2,

    /// <summary>
    /// If a <see cref="Quest"/> has been completed by the player, but rewards have yet to be collected.
    /// </summary>
    completed = 3,

    /// <summary>
    /// If a <see cref="Quest"/> has been finished by the player and rewards have been collected.
    /// </summary>
    finished = 4
};


/// <summary>
/// An object containing information of a single Quest. <br />
/// See also: <seealso cref="Quest_Manager"/>
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_default_steps</item>
///     <item>m_active_step_pos</item>
///     <item>m_quest_description</item>
///     <item>m_quest_name</item>
///     <item>m_quest_reward</item>
///     <item>m_quest_status</item>
///     <item>m_steps</item>
/// </list>
/// </summary>
public class Quest
{
    public readonly int m_default_steps = 50;
    public int m_active_step_pos;
    public string m_quest_description;
    public string m_quest_name;
    public Item.ItemType m_quest_reward;
    public QuestStatus m_quest_status;
    public List<QuestStep> m_steps;

    private int m_max_steps;

    /// <summary>
    ///
    /// </summary>
    /// <param name="quest_name">Name of the new Quest. Should be no more than 5 words</param>
    /// <param name="quest_description">Description of the quest.</param>
    /// <param name="quest_reward">Item the player will receive upon quest completion.</param>
    public Quest(string quest_name, string quest_description, QuestStatus status, Item.ItemType quest_reward)
    {
        m_steps = new List<QuestStep>();
        m_quest_status = status;
        m_quest_name = quest_name;
        m_quest_description = quest_description;
        m_quest_reward = quest_reward;
        m_active_step_pos = 0;
        m_max_steps = m_default_steps;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="step_name">The name of the step</param>
    /// <param name="step_description">A deeper description of what the player needs to do in order to complete
    ///                                 the step</param>
    /// <returns><list type="bullet">
    ///     <item><see langword="true"/> if there were no problems</item>
    ///     <item><see langword="false"/> if there was a problem adding the step</item>
    ///     </list></returns>
    public bool AddStep(string step_name, string step_description)
    {
        if (m_max_steps <= m_steps.Count)
        {
            //Debug.Log("Tried to add too many quest steps");
            return false;
        }

        QuestStep s = new QuestStep(step_name, step_description);
        m_steps.Add(s);

        return true;
    }

    /// <summary>
    /// Clears all steps from the quest
    /// </summary>
    public void ClearSteps()
    {
        m_steps.Clear();
    }

    /// <summary>
    /// Sends information to UI scripts to display quest information
    /// </summary>
    public void DisplayQuest()
    {
        QuestUI quest_ui = GameObject.Find("QuestUI").GetComponent<QuestUI>();
        quest_ui.AddQuest(this);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>
    ///     The current status of the quest
    ///     <seealso cref="QuestStatus"/>
    /// </returns>
    public QuestStatus GetStatus()
    {
        return m_quest_status;
    }

    public List<QuestStep> GetSteps()
    {
        return m_steps;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="step_position">the order the step should be placed in</param>
    /// <param name="step_name">The name of the step</param>
    /// <param name="step_description">A deeper description of what the player needs to do in order to complete
    ///                                 the step</param>
    /// <returns><list type="bullet">
    ///     <item><see langword="true"/> if there were no problems</item>
    ///     <item><see langword="false"/> if there was a problem adding the step</item>
    ///     </list></returns>
    public bool InsertStep(int step_position, string step_name, string step_description)
    {
        if (m_max_steps <= m_steps.Count)
        {
            //Debug.Log("Tried to add too many quest steps");
            return false;
        }

        QuestStep s = new QuestStep(step_name, step_description);
        m_steps.Insert(step_position, s);

        return true;
    }

    /// <summary>
    /// Proceeds to the next step of the quest
    /// </summary>
    /// <returns>
    ///
    /// <list type="bullet">
    ///     <item><see langword="true"/> if there is a new step</item>
    ///     <item><see langword="false"/> if the last step has been reached. Also sets status to complete.</item>
    /// </list>
    /// </returns>
    public bool NextStep()
    {
        int next_step = m_active_step_pos + 1;
        if (next_step > m_steps.Count)
        {
            Complete();
            return false;
        }
        m_active_step_pos = next_step;

        return true;
    }

    /// <summary>
    /// Removes the step at the given step position
    /// </summary>
    /// <param name="step_position"></param>
    /// <returns></returns>
    public bool RemoveStep(int step_position)
    {
        if (m_steps.Count <= 0)
        {
            return false;
        }

        m_steps.RemoveAt(step_position);
        return true;
    }

    /// <summary>
    /// Changes the index in the steps list to a new value.
    /// </summary>
    /// <param name="active_step_pos"></param>
    public void SetActiveStep(int active_step_pos)
    {
        m_active_step_pos = active_step_pos;
    }

    /// <summary>
    /// Takes all information of this class and creates a <see cref="QuestData"/> object containing it.
    /// </summary>
    /// <returns></returns>
    public QuestData ToSaveData()
    {
        // Add all required data to new save file
        QuestData save_quest = new QuestData();
        save_quest.m_quest_name = m_quest_name;
        save_quest.m_quest_description = m_quest_description;
        save_quest.m_quest_reward = m_quest_reward;
        save_quest.m_quest_status = m_quest_status;
        save_quest.m_active_step_pos = m_active_step_pos;

        //Get save data for each step
        List<StepData> steps_save_data = new List<StepData>();
        foreach (QuestStep step in m_steps)
        {
            steps_save_data.Add(step.ToSaveData());
        }

        save_quest.m_steps = steps_save_data;
        return save_quest;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="status">New status of the quest</param>
    public void UpdateStatus(QuestStatus status)
    {
        m_quest_status = status;
    }

    public void Complete()
    {
        PlayerClass player = PlayerClass.Instance;
        Item reward = new Item { itemType = m_quest_reward, amount = 1 };
        player.AddToInventory(reward);
        m_quest_status = QuestStatus.completed;
    }
}