/*
 * Filename: Inventory.cs
 * Developer: Kyle LeDoux
 * This file creates the inventory class. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Inventory Super Class
/// Add items, remove items, count items, use items on action, 
///  get item list
/// </summary>
public class Inventory
{
    public event EventHandler m_on_item_list_changed;

    private List<Item> m_item_list;
    private Action<Item> useItemAction;

    /// <summary>
    /// Constructor for Inventory class
    /// </summary>
    /// <param name="useItemAction"></param>
    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        m_item_list = new List<Item>();
    }
    /// <summary>
    /// Returns number of items in list
    /// </summary>
    /// <returns></returns>
    public int ItemCount()
    {
        int m_count = 0;
        foreach (Item i in m_item_list)
        {
            m_count++;
        }
        return m_count;
    }

    /// <summary>
    /// Adds item to inventory
    /// </summary>
    /// <param name="item"></param>
    /// <param name="inventoryCount"></param>
    public void AddItem(Item item, int inventoryCount)
    {
        int m_count = ItemCount();
        if (m_count < 18 && item.amount < 2000)
        {
            if (item.IsStackable())
            {
                bool itemAlreadyInInventory = false;
                foreach (Item inventoryItem in m_item_list)
                {
                    if (inventoryItem.itemType == item.itemType)
                    {
                        inventoryItem.amount += item.amount;
                        itemAlreadyInInventory = true;
                    }
                }
                if (!itemAlreadyInInventory)
                {
                    m_item_list.Add(item);
                }
            }
            else
            {
                m_item_list.Add(item);
            }
            m_on_item_list_changed?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Removed item from inventory
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in m_item_list)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                m_item_list.Remove(item);
                m_item_list.Remove(itemInInventory);
            }
        }
        else
        {
            if (m_item_list != null)
            {
                m_item_list.Remove(item);
            }
        }
        m_on_item_list_changed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Use action. Used when clicking
    /// </summary>
    /// <param name="item"></param>
    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    /// <summary>
    /// Counts items in list
    /// </summary>
    /// <returns></returns>
    public List<Item> GetItemList()
    {
        return m_item_list;
    }
}