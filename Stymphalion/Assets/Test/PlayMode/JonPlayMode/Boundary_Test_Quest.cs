using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Boundary_Test_Quest
{
    [Test]
    public void Boundary_Test_QuestUpper()
    {
        Quest q = new Quest("Quest", "Go Questing", "a cool sword");

        //Above upper bound
        for (int i = 0; i < 60; i++)
        {
            bool success = q.AddStep("step: " + i.ToString(), "step_descrip");
            if (i > 50)
            {
                Assert.IsFalse(success);
            }
            else
            {
                Assert.IsTrue(success);
            }
        }
        Assert.AreEqual(50, q.GetSteps().Count);

        //Within upper bound
        q.ClearSteps();

        for (int i = 0; i < 40; i++)
        {
            bool success = q.AddStep("step: " + i.ToString(), "step_descrip");
            Assert.IsTrue(success);
        }

        Assert.AreEqual(40, q.GetSteps().Count);

        //Right on upper bound
        q.ClearSteps();

        for (int i = 0; i < 50; i++)
        {
            bool success = q.AddStep("step: " + i.ToString(), "step_descrip");
            Assert.IsTrue(success);
        }

        Assert.AreEqual(50, q.GetSteps().Count);
    }

    [Test]
    public void Boundary_Test_QuestManager()
    {
        Quest_Manager quest_man = Quest_Manager.GetQuest_Manager();

        int num = 50;

        for (int i = 0; i < num; i++)
        {
            quest_man.AddQuest("name", "desc", "reward");
        }

        // Access out of bounds
        Quest over = quest_man.GetQuest(num + 40);
        Assert.IsNull(over);

        // Access in bounds
        Quest under = quest_man.GetQuest(num - num / 2);
        Assert.IsNotNull(under);

        // Access on boundary
        Quest on = quest_man.GetQuest(num - 1);
        Assert.IsNotNull(on);

        Quest negative = quest_man.GetQuest(-num);
        Assert.IsNull(negative);
    }
}