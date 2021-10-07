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
            bool success = q.AddStep(i, "step: " + i.ToString(), "step_descrip");
            if (i > 50)
            {
                Assert.IsFalse(success);
            }
        }
        Assert.AreEqual(50, q.GetSteps().Count);

        //Within upper bound
        q.ClearSteps();

        for (int i = 0; i < 40; i++)
        {
            bool success = q.AddStep(i, "step: " + i.ToString(), "step_descrip");
            Assert.IsTrue(success);
        }

        Assert.AreEqual(40, q.GetSteps().Count);

        //Right on upper bound
        q.ClearSteps();

        for (int i = 0; i < 50; i++)
        {
            bool success = q.AddStep(i, "step: " + i.ToString(), "step_descrip");
            Assert.IsTrue(success);
        }

        Assert.AreEqual(50, q.GetSteps().Count);
    }

    [Test]
    public void Boundary_Test_QuestLower()
    {
    }
}