using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object containing information of a single Quest. <br />
/// See also: <seealso cref="Quest_Manager"/>
/// </summary>
///
public class Quest
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
        /// If a <see cref="Quest"/> has been finished by the player.
        /// </summary>
        finished = 3
    };

    public string quest_name;
    public string quest_description;
    public string quest_reward; // Needs to be an item from kyles stuff

    private Status quest_status;
    private Quest_Step active_step;
    private int active_step_pos;
    private Dictionary<int, Quest_Step> steps; // This cannot be a dictionary because there is no notion of order. Go back to list or something.

    public Quest(string quest_name, string quest_description, string quest_reward)
    {
        steps = new Dictionary<int, Quest_Step>();
        this.quest_name = quest_name;
        this.quest_description = quest_description;
        this.quest_reward = quest_reward;
        active_step_pos = 0;
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
        foreach (Quest_Step step in steps.Values)
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

    /// <summary>
    /// Proceeds to the next step of the quest
    /// </summary>
    /// <returns>
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
            return false;
        }
        bool success = steps.TryGetValue(active_step_pos + 1, out active_step);
        if (!success)
        {
            quest_status = Status.finished;
        }

        return success;
    }

    public Dictionary<int, Quest_Step> GetSteps()
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
    public bool AddStep(int step_position, string step_name, string step_description)
    {
        if (max_steps <= steps.Count)
        {
            //Debug.Log("Tried to add too many quest steps");
            return false;
        }

        Quest_Step s = new Quest_Step(step_name, step_description, this);
        steps.Add(step_position, s);

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

        return steps.Remove(step_position);
    }

    /// <summary>
    /// Clears all steps from the quest
    /// </summary>
    public void ClearSteps()
    {
        steps.Clear();
    }
}