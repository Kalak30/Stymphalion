using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    private Inventory inventory;

    public void HealthPotion()
    {
        GameObject m_ui_shop;
        GameObject m_shop;
        GameObject m_items;

        m_ui_shop = GameObject.Find("UI_Shop");
        Debug.Log("HERE");
        m_ui_shop.SetActive(!m_ui_shop.activeSelf);

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

    public void Medkit()
    {
        PlayerClass m_player = PlayerClass.Instance;
        Item reward = new Item { itemType = Item.ItemType.Medkit, amount = 1 };
        Item cost = new Item { itemType = Item.ItemType.Gold, amount = 1 };
        int count = m_player.CountGold(cost);
        Debug.Log(count);
        if (count >= 75)
        {
            for (int i = 0; i < 75; i++)
            {
                m_player.RemoveFromInventory(cost);
            }
            m_player.AddToInventory(reward);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }
}