using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Step
{
    public string step_name, step_description;
    public Quest belongs_to;

    public Quest_Step(string step_name, string step_description, Quest belongs_to)
    {
        this.step_name = step_name;
        this.step_description = step_description;
        this.belongs_to = belongs_to;
    }

    public void DisplayStep()
    {
        Debug.Log("Step Name: " + step_name);
        Debug.Log("Step Description: " + step_description);
        return;
    }

    public Step_Data ToSaveData()
    {
        Step_Data save_data = new Step_Data();
        save_data.step_name = step_name;
        save_data.step_description = step_description;

        return save_data;
    }
}