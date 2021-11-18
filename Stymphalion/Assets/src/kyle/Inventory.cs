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
/// 
/// </summary>
public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private Action<Item> useItemAction;
    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
    }

    public int ItemCount()
    {
        int m_count = 0;
        foreach (Item i in itemList)
        {
            m_count++;
        }
        return m_count;
    }

    public void AddItem(Item item, int inventoryCount)
    {
        int m_count = ItemCount();
        if (m_count < 18 && item.amount < 2000)
        {
            if (item.IsStackable())
            {
                bool itemAlreadyInInventory = false;
                foreach (Item inventoryItem in itemList)
                {
                    if (inventoryItem.itemType == item.itemType)
                    {
                        inventoryItem.amount += item.amount;
                        itemAlreadyInInventory = true;
                    }
                }
                if (!itemAlreadyInInventory)
                {
                    itemList.Add(item);
                }
            }
            else
            {
                itemList.Add(item);
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(item);
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            if (itemList != null)
            {
                itemList.Remove(item);
            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}