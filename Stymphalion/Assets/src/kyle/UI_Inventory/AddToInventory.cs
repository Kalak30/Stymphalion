using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddToInventory : MonoBehaviour
{
    private Inventory inventory;
    public GameObject m_ui_shop;
    public GameObject m_shop;
    public GameObject m_ui;
    public GameObject m_player_gold;

    private void Awake()
    {
        m_ui_shop = GameObject.Find("UI_Shop");
        m_shop = m_ui_shop.transform.Find("Shop").gameObject;
        m_ui = m_shop.transform.Find("UI").gameObject;
        m_player_gold = m_ui.transform.Find("PlayerGold").gameObject;
    }
    public void HealthPotion()
    {
        TextMeshProUGUI uiText = m_player_gold.GetComponent<TextMeshProUGUI>();

        PlayerClass m_player = PlayerClass.Instance;
        Item reward = new Item { itemType = Item.ItemType.HealthPotion, amount = 1 };
        Item cost = new Item { itemType = Item.ItemType.Gold, amount = 1 };
        uiText.SetText(m_player.CountGold(cost).ToString());
        int count = m_player.CountGold(cost);
        if (count >= 10)
        {
            for (int i = 0; i < 10; i++)
            {
                m_player.RemoveFromInventory(cost);
                count = m_player.CountGold(reward);
            }
            m_player.AddToInventory(reward);
            uiText.SetText(m_player.CountGold(cost).ToString());
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    public void Medkit()
    {
        TextMeshProUGUI uiText = m_player_gold.GetComponent<TextMeshProUGUI>();

        PlayerClass m_player = PlayerClass.Instance;
        Item reward = new Item { itemType = Item.ItemType.Medkit, amount = 1 };
        Item cost = new Item { itemType = Item.ItemType.Gold, amount = 1 };
        uiText.SetText(m_player.CountGold(cost).ToString());
        int count = m_player.CountGold(cost);
        if (count >= 75)
        {
            for (int i = 0; i < 75; i++)
            {
                m_player.RemoveFromInventory(cost);
            }
            m_player.AddToInventory(reward);
            uiText.SetText(m_player.CountGold(cost).ToString());
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }
}
