using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory{
    private List<Item> itemList;
        
        public void InventoryManage(){
            itemList = new List<Item>();

            AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
            RemoveItem(new Item{itemType = Item.ItemType.Sword, amount = 1}, itemList.Count);
        }

        
        public void AddItem(Item item, int inventoryCount){
            if( inventoryCount < 10 ){
                itemList.Add(item);
            }
        }
        public void RemoveItem(Item item, int inventoryCount){
            if( inventoryCount > 0 ){
                itemList.Remove(item);
            } 
        }
    

        public void InventoryCreation(){
            Debug.Log("Inventory not yet set up");
            Debug.Log("Inventory will call function from Inventory shown below");
    }
}