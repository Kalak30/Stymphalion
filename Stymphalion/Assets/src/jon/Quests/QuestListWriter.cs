/*
 * Filename: QuestListWriter.cs
 * Developer: Jon Kopf
 * Purpose: Write quest data to the persistent data path
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListWriter
{
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
    /// Turns a QuestsListData object into a Json string
    /// </summary>
    /// <param name="q_data">Data to be Jsonified</param>
    /// <returns>String of json objects</returns>
    public string QuestJSON(QuestsListData q_data)
    {
        return JsonUtility.ToJson(q_data, true);
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
}