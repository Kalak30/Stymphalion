using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        ItemWorld.SpawnItemWorld(new Vector3(4, 3), new Item { itemType = Item.ItemType.HealthPotion, amount = 2 }); 
        ItemWorld.SpawnItemWorld(new Vector3(0, 3), new Item { itemType = Item.ItemType.Gold, amount = 200 });
        ItemWorld.SpawnItemWorld(new Vector3(1, 3), new Item { itemType = Item.ItemType.Medkit, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 4), new Item { itemType = Item.ItemType.Bow, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 5), new Item { itemType = Item.ItemType.Krotola, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 6), new Item { itemType = Item.ItemType.HydraBlood, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 7), new Item { itemType = Item.ItemType.QuestItem, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 8), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(1, 3), new Item { itemType = Item.ItemType.Medkit, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 4), new Item { itemType = Item.ItemType.Bow, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 5), new Item { itemType = Item.ItemType.Krotola, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 6), new Item { itemType = Item.ItemType.HydraBlood, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 7), new Item { itemType = Item.ItemType.QuestItem, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 8), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(1, 3), new Item { itemType = Item.ItemType.Medkit, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 4), new Item { itemType = Item.ItemType.Bow, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 5), new Item { itemType = Item.ItemType.Krotola, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 6), new Item { itemType = Item.ItemType.HydraBlood, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 7), new Item { itemType = Item.ItemType.QuestItem, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 8), new Item { itemType = Item.ItemType.Sword, amount = 1 });


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
