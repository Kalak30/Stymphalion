using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class UIBoundaryTest
{
    [UnityTest]
    public IEnumerator UIWidthBoundaryTest()
    {
        SceneManager.LoadScene("MainIsland", LoadSceneMode.Single);

        // Wait for scene to be loaded before setting it to be active
        while (!SceneManager.GetSceneByName("MainIsland").isLoaded)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainIsland"));
        QuestManager.GetQuestManager().Load();
        QuestUI quest_ui = GameObject.Find("QuestUI").GetComponent<QuestUI>();

        for (int i = 0; i < 2000; i++)
        {
            quest_ui.AddQuest(new Quest("this is a quest name", "this is a very long quest description", QuestStatus.active, Item.ItemType.Medkit));
        }

        TestDelegate test_delegate = () => quest_ui.Test();
        Assert.DoesNotThrow(test_delegate, "The width of Quest UI text is too large");

        quest_ui.ToggleDisplay();
    }
}