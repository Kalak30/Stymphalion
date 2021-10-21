using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object containing information of a single Quest. <br />
/// See also: <seealso cref="Quest_Manager"/>
/// </summary>
///
public class Quest : ScriptableObject
{
    public readonly int DEFAULT_STEPS = 50;
    private int max_steps;

    /// <summary>
    /// Status of a <see cref="Quest"/>
    /// </summary>
    public enum Status
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

    public string quest_name;
    public string quest_description;
    public Item quest_reward;  // Needs to be an item from kyles stuff

    public Status quest_status;
    public int active_step_pos;
    public List<Quest_Step> steps;

    public Quest(string quest_name, string quest_description, Item quest_reward)
    {
        steps = new List<Quest_Step>();
        this.quest_name = quest_name;
        this.quest_description = quest_description;
        this.quest_reward = quest_reward;
        this.active_step_pos = 0;
        max_steps = DEFAULT_STEPS;
    }

    /// <summary>
    /// Sends information to UI scripts to display quest information
    /// </summary>
    public void DisplayQuest()
    {
        Debug.Log("Quest Name: " + quest_name);
        Debug.Log("Quest Description: " + quest_description);
        Debug.Log("Quest Reward: " + quest_reward);
        foreach (Quest_Step step in steps)
        {
            step.DisplayStep();
        }

        return;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>
    ///     The current status of the quest
    ///     <seealso cref="Status"/>
    /// </returns>
    public Status GetStatus()
    {
        return quest_status;
    }

    public void UpdateStatus(Status status)
    {
        quest_status = status;
    }

    public void SetActiveStep(int active_step_pos)
    {
        this.active_step_pos = active_step_pos;
    }

    /// <summary>
    /// Proceeds to the next step of the quest
    /// </summary>
    /// <returns>
    ///
    /// <list type="bullet">
    ///     <item><see langword="true"/> if there is a new step</item>
    ///     <item><see langword="false"/> if the last step has been reached. Also sets status to finished.</item>
    /// </list>
    /// </returns>
    public bool NextStep()
    {
        int next_step = active_step_pos + 1;
        if (next_step > max_steps)
        {
            quest_status = Status.completed;
            return false;
        }
        active_step_pos = next_step;

        return true;
    }

    public List<Quest_Step> GetSteps()
    {
        return steps;
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
        if (max_steps <= steps.Count)
        {
            //Debug.Log("Tried to add too many quest steps");
            return false;
        }

        Quest_Step s = new Quest_Step(step_name, step_description, this);
        steps.Insert(step_position, s);

        return true;
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
        if (max_steps <= steps.Count)
        {
            //Debug.Log("Tried to add too many quest steps");
            return false;
        }

        Quest_Step s = new Quest_Step(step_name, step_description, this);
        steps.Add(s);

        return true;
    }

    /// <summary>
    /// Removes the step at the given step position
    /// </summary>
    /// <param name="step_position"></param>
    /// <returns></returns>
    public bool RemoveStep(int step_position)
    {
        if (steps.Count <= 0)
        {
            return false;
        }

        steps.RemoveAt(step_position);
        return true;
    }

    /// <summary>
    /// Clears all steps from the quest
    /// </summary>
    public void ClearSteps()
    {
        steps.Clear();
    }

    public Quest_Data ToSaveData()
    {
        // Add all required data to new save file
        Quest_Data save_quest = new Quest_Data();
        save_quest.quest_name = quest_name;
        save_quest.quest_description = quest_description;
        save_quest.quest_reward = quest_reward;
        save_quest.quest_status = (int)quest_status;
        save_quest.active_step_pos = active_step_pos;

        //Get save data for each step
        List<Step_Data> steps_save_data = new List<Step_Data>();
        foreach (Quest_Step step in steps)
        {
            steps_save_data.Add(step.ToSaveData());
        }

        save_quest.steps = steps_save_data;
        return save_quest;
    }
}