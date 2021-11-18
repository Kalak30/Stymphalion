using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class NPCStressTest
{
   

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NPCStressTestWithEnumeratorPasses()
    {
        SceneManager.LoadScene("MainIsland");
        yield return new WaitForSeconds(2);

        NPCManager manager = NPCManager.GetSingleton();

        int m_count_npc = 0;
        while (true)
        {
            manager.AddQuestNPC(1);
            m_count_npc++;
            yield return new WaitForSeconds(0.1f);
            if((1/Time.deltaTime) < 30f)
            {
                break;
            }
        }

        Debug.Log($"Max NPCs before fps dropped below 30: {m_count_npc}");
        yield return null;
    }
}
