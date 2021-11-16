using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMaster : MonoBehaviour
{
    public void AddPotion()
    {
        PlayerClass player = PlayerClass.Instance;
        Item reward = new Item { itemType = Item.ItemType.HealthPotion, amount = 1 };
        player.AddToInventory(reward);
        Debug.Log("Success");
    }

}
