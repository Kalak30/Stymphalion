using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Stress_Test_Quests
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Stress_Test_QuestsWithEnumeratorPasses()
    {
        SceneManager.LoadScene("Main Island");
        for (int i = 0; i > -1; i++)
        {
            //Create a bunch of new quests within the quest manager
            Object.Instantiate(Resources.Load("Assets/prefabs/jon/NPC"));
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}