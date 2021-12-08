using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Code for setting the button actions in shop UI
/// </summary>
public class AddToInventory : MonoBehaviour
{
    private Inventory m_inventory;
    public GameObject m_ui_shop;
    public GameObject m_shop;
    public GameObject m_ui;
    public GameObject m_player_gold;

    /// <summary>
    /// Finds the text GameObject so player's gold can be updated
    /// </summary>
    private void Awake()
    {
        m_ui_shop = GameObject.Find("UI_Shop");
        m_shop = m_ui_shop.transform.Find("Shop").gameObject;
        m_ui = m_shop.transform.Find("UI").gameObject;
        m_player_gold = m_ui.transform.Find("PlayerGold").gameObject;
    }

    /// <summary>
    /// Activated when button is pressed over health potion in shop
    ///     Changes the player's gold
    ///     Subtracts 10 gold from player
    ///     Will not update inventory with item if player doesn't have enough gold
    /// </summary>
    public void HealthPotion()
    {
        TextMeshProUGUI uiText = m_player_gold.GetComponent<TextMeshProUGUI>();

        PlayerClass m_player = PlayerClass.Instance;
        Item reward = new Item { itemType = Item.ItemType.HealthPotion, amount = 1 };
        Item cost = new Item { itemType = Item.ItemType.Gold, amount = 1 };
        uiText.SetText(m_player.CountGold(cost).ToString());
        int count = m_player.CountGold(cost);
        if (count >= 30)
        {
            for (int i = 0; i < 30; i++)
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

    /// <summary>
    /// Button function for when medkit is clicked in the shop
    ///     Find's player's gold
    ///     Changes UI for player's gold when purchased
    ///     Won't be purchased if player doesn't have enough gold
    /// </summary>
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
