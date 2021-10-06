using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Stress_Test_Quests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Stress_Test_QuestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Stress_Test_QuestsWithEnumeratorPasses()
    {
        SceneManager.LoadScene("Assets/tst/ross/Main Island.unity");
        for (int i = 0; i > -1; i++)
        {
            //Create a bunch of new quests within the quest manager
            Object.Instantiate(Resources.Load("Assets/prefabs/jon/Quest_NPC_Pre.prefab"));
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}