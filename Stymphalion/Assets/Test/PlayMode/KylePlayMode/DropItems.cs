using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class DropItems
{
    [UnityTest]
    public IEnumerator DropItem()
    {
        SceneManager.LoadScene("Mainisland");
        yield return new WaitForSeconds(2);

        PlayerClass m_player = PlayerClass.Instance;
        Item item = new Item { itemType = Item.ItemType.HealthPotion, amount = 1 };
        int m_count_items = 0;
        int x = 0;
        while(x == 0)
        {
            ItemWorld.DropItem(m_player.GetLocation(), item);
            m_count_items++;
            yield return new WaitForSeconds(0.01f);
            if((1/Time.deltaTime) < 30f)
            {
                x = 1;
            }
        }
        Debug.Log("Max items before fps droppeed below 30: " + m_count_items);

    }

}
