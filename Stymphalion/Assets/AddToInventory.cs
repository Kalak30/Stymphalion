using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    private Inventory inventory;

    public void HealthPotion()
    {
        PlayerClass m_player = PlayerClass.Instance;
        Item reward = new Item { itemType = Item.ItemType.HealthPotion, amount = 1 };
        Item cost = new Item { itemType = Item.ItemType.Gold, amount = 1 };
        int count = m_player.CountGold(cost);
        if (count >= 10)
        {
            for (int i = 0; i < 10; i++)
            {
                m_player.RemoveFromInventory(cost);
                count = m_player.CountGold(reward);
            }
            m_player.AddToInventory(reward);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }
}
