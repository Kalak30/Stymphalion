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
///     <item>m_quest_builder</item>
///     <item>m_file_contents</item>
/// </list>
/// </summary>
public class QuestListReader
{

    public TextAsset m_file;
    private string m_file_contents;
    private QuestListBuilder m_quest_builder;

    /// <summary>
    ///
    /// </summary>
    /// <param name="quest_builder">QuestBuilder registered with this QuestReader object</param>
    public QuestListReader(QuestListBuilder quest_builder)
    {
        m_quest_builder = quest_builder;
    }

    public void Construct(string file_name)
    {
        QuestsListData quest_list = ConstructData(file_name);
        m_quest_builder.BuildQuests(quest_list);
    }

    public QuestsListData ConstructData(string file_name)
    {
        string file_contents = ReadFile(file_name);
        QuestsListData quest_list = JsonUtility.FromJson<QuestsListData>(file_contents);
        return quest_list;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="file_name"></param>
    private string ReadFile(string file_name)
    {
        string file_contents;
        // Load the default quest data
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1 || !File.Exists(Application.persistentDataPath + "/" + file_name + ".txt"))
        {
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            file_contents = m_file.text;
        }
        else
        {
            Debug.Log("NOT first time opening");

            FileManager.LoadFromFile(file_name + ".txt", out file_contents);
        }

        return file_contents;
    }
}