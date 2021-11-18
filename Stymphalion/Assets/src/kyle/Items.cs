using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates items on load
///     Lists all items
/// </summary>
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

    /// <summary>
    /// Assigns sprite to item
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:        return ItemAssets.Instance.m_sword_sprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.m_health_potion_sprite;
            case ItemType.Gold:         return ItemAssets.Instance.m_gold_sprite;
            case ItemType.Medkit:       return ItemAssets.Instance.m_medkit_sprite;
            case ItemType.Bow:          return ItemAssets.Instance.m_bow_sprite;
            case ItemType.Krotola:      return ItemAssets.Instance.m_krotola_sprite;
            case ItemType.QuestItem:    return ItemAssets.Instance.m_quest_item_sprite;
            case ItemType.HydraBlood:   return ItemAssets.Instance.m_hydra_blood_sprite;
        }
    }

    /// <summary>
    /// Checks to see if an item is stackable
    /// </summary>
    /// <returns></returns>
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
