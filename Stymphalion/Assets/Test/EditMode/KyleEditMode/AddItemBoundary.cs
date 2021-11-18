using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class AddItemBoundaryTest{
    public class Item{
        public enum ItemType{
            Sword,
            gold,
            HealthPotion
        }
        public ItemType itemType;
        public int amount;

    }
    public class Inventory{
        private List<Item> itemList;
        
        [Test]
        public void InventoryManage(){
            itemList = new List<Item>();
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            RemoveItem(new Item{itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            Assert.IsTrue(itemList.Count <= 10);
        }

        
        public void AddItem(Item item, int inventoryCount){
            if( inventoryCount < 10 ){
                inventoryCount++;
                itemList.Add(item);
            }
        }
        public void RemoveItem(Item item, int inventoryCount){
            Debug.Log("Item List Count after Add: ");
            Debug.Log(itemList.Count);
            if( inventoryCount > 0 ){
                itemList.Remove(item);
            } 
        }
    }
}
