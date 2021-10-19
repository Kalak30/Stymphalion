using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quests_List_Data
{
    public List<Quest_Data> quests;
}

[System.Serializable]
public class Quest_Data
{
    public string quest_name;
    public string quest_description;
    public Item quest_reward;  // Needs to be an item from kyles stuff

    public int quest_status;
    public int active_step_pos;
    public List<Step_Data> steps;
}

[System.Serializable]
public class Step_Data
{
    public string step_name;
    public string step_description;
}