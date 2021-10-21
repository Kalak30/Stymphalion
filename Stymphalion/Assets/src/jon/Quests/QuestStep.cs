/*
 * Filename: QuestStep.cs
 * Developer: Jon Kopf
 * Purpose: A description of a single step within a quest
 */

using UnityEngine;

/// <summary>
/// A single step within a quest
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_step_name</item>
///     <item>m_step_description</item>
/// </list>
/// </summary>
public class QuestStep
{
    public string m_step_name, m_step_description;

    /// <summary>
    ///
    /// </summary>
    /// <param name="step_name">Name of new step. </param>
    /// <param name="step_description">Detailed description of new step. </param>
    public QuestStep(string step_name, string step_description)
    {
        m_step_name = step_name;
        m_step_description = step_description;
    }

    /// <summary>
    /// Puts the <see cref="Step"/> onto the screen for the player to see
    /// </summary>
    public void DisplayStep()
    {
        Debug.Log("Step Name: " + m_step_name);
        Debug.Log("Step Description: " + m_step_description);
        return;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>
    /// <see cref="StepData"/> object containing all data required to characterize a <see cref="QuestStep"/>
    /// </returns>
    public StepData ToSaveData()
    {
        StepData save_data = new StepData();
        save_data.m_step_name = m_step_name;
        save_data.m_step_description = m_step_description;

        return save_data;
    }
}