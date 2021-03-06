using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Stress_Test_Quests
{
    private QuestManager qm = QuestManager.GetQuestManager();



    // Still have no idea for a stress test
    [UnityTest]
    public IEnumerator Stress_Test_QuestManager()
    {
        while ((1 / Time.smoothDeltaTime) > 30)
        {
            for (int i = 0; i < 1000; i++)
            {
                Quest q = new Quest("i", "i", QuestStatus.active, Item.ItemType.HealthPotion);
                qm.AddQuest(q);
            }
            yield return null;
        }
        Debug.Log($"Quests created before frames dropped below 30: {qm.QuestsLength()}");
        Assert.IsTrue((1.0f / Time.smoothDeltaTime) < 30);
        //Assert
        yield return null;
    }
}