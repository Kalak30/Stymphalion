using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class AddItemBoundary
{
    [UnityTest]
    public IEnumerator InventoryMax()
    {
        SceneManager.LoadScene("Mainisland");
        yield return new WaitForSeconds(2);

        PlayerClass m_player = PlayerClass.Instance;
        GameObject m_temp = GameObject.Find("UI_Inventory");
        GameObject m_ui_inventory = m_temp.transform.Find("Inventory").gameObject;
        //m_ui_inventory.SetActive(true);

        Item item = new Item { itemType = Item.ItemType.Sword, amount = 1 };
        int x = 0;
        while (x < 30)
        {
            //ItemWorld.SpawnItemWorld(m_player.GetLocation(), item);
            m_player.AddToInventory(item);
            yield return new WaitForSeconds(0.1f);
            x++;
        }
        int a = m_player.CountItemsInInventory();
        Assert.AreEqual(18, a);
    }

}