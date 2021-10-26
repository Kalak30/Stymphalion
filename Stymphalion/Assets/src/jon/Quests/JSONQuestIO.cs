/*
 * Filename: JSONQuestIO.cs
 * Developer: Jon Kopf
 * Purpose: The purpose of this file is to give the Quest Manager a way to save and load data about quests.
 */

using System.IO;
using UnityEngine;


/// <summary>
/// Allows for Quests to be written and read from data store.
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_file</item>
///     <item>m_json_quest_reader</item>
///     <item>m_file_contents</item>
/// </list>
/// </summary>
public class JSONQuestIO
{

    public TextAsset m_file;
    private static JSONQuestIO m_json_quest_reader;
    private string m_file_contents;

    /// <summary>
    ///
    /// </summary>
    /// <returns>Returns the static instance of <see cref="JSONQuestIO"/>. This provides the functionality of a singleton pattern</returns>
    public static JSONQuestIO GetReader()
    {
        if (m_json_quest_reader == null)
        {
            m_json_quest_reader = new JSONQuestIO();
        }

        return m_json_quest_reader;
    }

    /// <summary>
    /// Turns a QuestsListData object into a Json string
    /// </summary>
    /// <param name="q_data">Data to be Jsonified</param>
    /// <returns>String of json objects</returns>
    public string QuestJSON(QuestsListData q_data)
    {
        return JsonUtility.ToJson(q_data, true);
    }

    /// <summary>
    /// Turns the Quests in the QuestManager into a json string
    /// </summary>
    /// <returns>New Json string in a prettified mode (extra whitespace) </returns>
    public string QuestJSON()
    {
        string json = JsonUtility.ToJson(QuestManager.GetQuestManager().ToSaveData(), true);
        Debug.Log("JSON: " + json);
        return json;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="file_name"></param>
    public void ReadFile(string file_name)
    {
        // Load the default quest data
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1 || !File.Exists(Application.persistentDataPath + "/" + file_name + ".txt"))
        {
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            m_file_contents = m_file.text;
        }
        else
        {
            Debug.Log("NOT first time opening");

            FileManager.LoadFromFile(file_name + ".txt", out m_file_contents);
        }

        CreateDataStructures();
    }

    /// <summary>
    /// Reads a quest file, and changes qld to be a list of quest data matching the read file
    /// </summary>
    /// <param name="file_name"> name of file in the persistent data path. no file extention is required</param>
    /// <param name="qld">Quest list data that should be returned</param>
    public void ReadFile(string file_name, out QuestsListData qld)
    {
        // Load the default quest data
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1 || !File.Exists(Application.persistentDataPath + "/" + file_name + ".txt"))
        {
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            m_file_contents = m_file.text;
        }
        else
        {
            Debug.Log("NOT first time opening");

            FileManager.LoadFromFile(file_name + ".txt", out m_file_contents);
        }

        qld = JsonUtility.FromJson<QuestsListData>(m_file_contents);
    }

    /// <summary>
    /// Saves the <see cref="QuestsListData""/> to a file in the persistent data path.
    /// </summary>
    /// <param name="file_name">file name (not including file extension) </param>
    /// <param name="q_data">Quest Data to be saved</param>
    public void SaveFile(string file_name, QuestsListData q_data)
    {
        FileManager.WriteToFile(file_name + ".txt", QuestJSON(q_data));
    }

    /// <summary>
    /// Saves the current quest information in <see cref="Quest_Manager"/> to a file in the persistent data path.
    /// </summary>
    /// <param name="file_name">file name (not including file extension)</param>
    public void SaveFile(string file_name)
    {
        FileManager.WriteToFile(file_name + ".txt", QuestJSON());
    }


    /// <summary>
    /// Creates new <see cref="QuestsListData"/>, <see cref="QuestData"/>. and <see cref="StepData"/>
    /// so they can then turned into a JSON file
    /// </summary>
    private void CreateDataStructures()
    {
        QuestsListData quest_list = JsonUtility.FromJson<QuestsListData>(m_file_contents);
        foreach (QuestData quest in quest_list.m_quests)
        {
            Quest new_quest = new Quest(quest.m_quest_name, quest.m_quest_description, quest.m_quest_reward);
            new_quest.SetActiveStep(quest.m_active_step_pos);
            new_quest.UpdateStatus((QuestStatus)quest.m_quest_status);

            foreach (StepData step in quest.m_steps)
            {
                new_quest.AddStep(step.m_step_name, step.m_step_description);
            }

            QuestManager.GetQuestManager().AddQuest(new_quest);
        }
    }
}