using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Stress_Test_Quests
{
    private Quest_Manager qm = Quest_Manager.GetQuest_Manager();

    // Still have no idea for a stress test
    [UnityTest]
    public IEnumerator Stress_Test_QuestManager()
    {
        

        
        for (int i = 0; i <= qm.QuestsLength(); i++)
        {
            qm.AddQuest("i","i","i");
           // MakeQuests();
            //qm.DisplayQuests();
        }
        //Assert 
        yield return null;
    }

    private void MakeQuests()
    {
        int quests_per = 30000;
        for (int i = 0; i < quests_per; i++)
        {
            // Make the strings longer just because I can
            Quest q = new Quest("a".PadLeft(i, 'c'), "a".PadRight(i, 'b'), "a".PadLeft(i, 'a'));
            MakeSteps(q);
            qm.AddQuest(q);
        }
    }

    private void MakeSteps(Quest q)
    {
        int steps_per = 4000;
        for (int i = 0; i < steps_per; i++)
        {
            q.AddStep("s".PadLeft(i, 't'), "u".PadLeft(i, 'v'));
        }
    }
}