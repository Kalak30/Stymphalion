/*
 * Filename: Inventory.cs
 * Developer: Kyle LeDoux
 * This file creates the inventory class. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Inventory
{
    private List<Item> itemList;
    private GameObject m_ui_inventory;

    private void Start()
    {
        m_ui_inventory = GameObject.Find("UI_Inventory");
    }

    public void InventoryManage()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 }, itemList.Count);
        RemoveItem(new Item { itemType = Item.ItemType.Sword, amount = 1 }, itemList.Count);
    }

    /// <summary>
    /// Adds item to inventory. 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="inventoryCount"></param>
    public void AddItem(Item item, int inventoryCount)
    {
        if (inventoryCount < 10)
        {
            itemList.Add(item);
        }
    }
    public void RemoveItem(Item item, int inventoryCount)
    {
        if (inventoryCount > 0)
        {
            itemList.Remove(item);
        }
    }

    public void ToggleInventory()
    {
        m_ui_inventory.SetActive(!m_ui_inventory.activeSelf);
        Debug.Log("Inventory not yet set up");
        Debug.Log("Inventory will call function from Inventory shown below");
    }
}