using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class JSON_Quest_Reader : MonoBehaviour
{
    public TextAsset file;
    private string file_contents;

    //The ReadFile() method should be called elsewhere. Possibly in some of
    // Ross' code during boot.
    public void Start()
    {
        ReadFile("quests");
        Quest_Manager.GetQuest_Manager().AddQuest("another quest", "another description", "the coolest sword ever");
        SaveFile("quests");
        Debug.Log("++++++++++++++++++\n++++++++++++++++");
        ReadFile("quests");
    }

    public void SaveFile(string file_name)
    {
        FileManager.WriteToFile(file_name + ".txt", QuestJSON());
    }

    public string QuestJSON()
    {
        string json = JsonUtility.ToJson(Quest_Manager.GetQuest_Manager().ToSaveData(), true);
        Debug.Log("JSON: " + json);
        return json;
    }

    public void ReadFile(string file_name)
    {
        // Load the default quest data
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            file_contents = file.text;
        }
        else
        {
            Debug.Log("NOT first time opening");

            FileManager.LoadFromFile(file_name + ".txt", out file_contents);
        }

        CreateDataStructures();
    }

    public void CreateDataStructures()
    {
        Quests_List_Data quest_list = JsonUtility.FromJson<Quests_List_Data>(file_contents);
        foreach (Quest_Data quest in quest_list.quests)
        {
            Quest new_quest = new Quest(quest.quest_name, quest.quest_description, quest.quest_reward);
            new_quest.SetActiveStep(quest.active_step_pos);
            new_quest.UpdateStatus((Quest.Status)quest.quest_status);

            Debug.Log($"Quest Name: {quest.quest_name} \nQuest Description: {quest.quest_description}");
            Debug.Log($"Quest Reward: {quest.quest_reward} \nQuest Status: {quest.quest_status}");
            Debug.Log($"Active Step Position: {quest.active_step_pos}");

            foreach (Step_Data step in quest.steps)
            {
                new_quest.AddStep(step.step_name, step.step_description);

                Debug.Log("\tStep Name: " + step.step_name + "\n\tStep Description: " + step.step_description);
            }

            Quest_Manager.GetQuest_Manager().AddQuest(new_quest);
        }
    }
}