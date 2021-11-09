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
    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 }, itemList.Count);
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 }, itemList.Count);
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 }, itemList.Count);
        Debug.Log(itemList.Count);
        Debug.Log("inventory");
    }
    public void InventoryManager()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 }, itemList.Count);
        Debug.Log("Manager");
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 }, itemList.Count);
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item, int inventoryCount)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}