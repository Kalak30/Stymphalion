using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Step : MonoBehaviour
{
    public struct Step_List : IEnumerable<Quest_Step>
    {
        public readonly Quest_Step FINISH;
        private List<Quest_Step> steps;
        private int current_index;

        public Step_List(Quest_Step first)
        {
            FINISH = new Quest_Step("Finish", "Finish", null);
            steps = new List<Quest_Step>();
            steps.Add(first);
            current_index = 0;
        }

        public Quest_Step next()
        {
            if (current_index + 1 == steps.Count)
            {
                return steps[++current_index];
            }
            else
            {
                return FINISH;
            }
        }

        public void AddStep(Quest_Step step)
        {
            steps.Add(step);
        }

        public IEnumerator<Quest_Step> GetEnumerator()
        {
            return steps.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public Quest_Step(string step_name, string step_description, Quest belongs_to)
    {
        this.step_name = step_name;
        this.step_description = step_description;
        this.belongs_to = belongs_to;
    }

    public string step_name;
    public string step_description;
    private Quest belongs_to;

    public void DisplayStep()
    {
        Debug.Log("Step Name: " + step_name);
        Debug.Log("Step Description: " + step_description);
        return;
    }
}