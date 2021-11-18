using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

/// <summary>
/// Object Pool pattern implimented with "pfItemWorld" prefab
/// Reusable: ItemAssets.Instance.pfItemWorld
/// Functions: 
///     Static SpawnItem(Vector3, Item)
///     Static Drop Item(Vector3, Item)
///     SetItem(Item)
///     GetItem()
///     DestroySelf()
/// </summary>

///  <summary>
///  Creates itemworld prefab functions
///  </summary>
public class ItemWorld : MonoBehaviour
{
    /// <summary>
    /// Spawns an item in the world with a given location
    /// </summary>
    /// <param name="position"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }
     
    /// <summary>
    /// Drops the item. Used when dropping from inventory
    /// </summary>
    /// <param name="dropPosition"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 1f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 1f, ForceMode2D.Impulse);
        return itemWorld;
    }

    private Item m_item;
    private SpriteRenderer m_sprite_renderer;
    private TextMeshPro m_textMeshPro;

    /// <summary>
    /// Finds sprite and amount of item components
    /// </summary>
    private void Awake() 
    {
        m_sprite_renderer = GetComponent<SpriteRenderer>();
        m_textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// Sets the sprite and amount to prefab component
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(Item item)
    {
        this.m_item = item;
        m_sprite_renderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            m_textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            m_textMeshPro.SetText("");
        }
    }

    /// <summary>
    /// Returns item prefab
    /// </summary>
    /// <returns></returns>
    public Item GetItem()
    {
        return m_item;
    }

    /// <summary>
    /// Destroys item. Used when player walks over item and is added to inventory
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}