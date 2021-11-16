using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{
    public enum ItemType{
        Sword,
        Gold,
        HealthPotion,
        Medkit,
        Bow,
        Krotola,
        QuestItem,
        HydraBlood,
    }
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:        return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.Gold:         return ItemAssets.Instance.goldSprite;
            case ItemType.Medkit:       return ItemAssets.Instance.medkitSprite;
            case ItemType.Bow:          return ItemAssets.Instance.bowSprite;
            case ItemType.Krotola:      return ItemAssets.Instance.krotolaSprite;
            case ItemType.QuestItem:    return ItemAssets.Instance.questItemSprite;
            case ItemType.HydraBlood:   return ItemAssets.Instance.hydraBloodSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Gold:
            case ItemType.HealthPotion:
                return true;
            case ItemType.Sword:
            case ItemType.Medkit:
            case ItemType.Bow:
            case ItemType.Krotola:
            case ItemType.QuestItem:
            case ItemType.HydraBlood:
                return false;
        }
    }
}
