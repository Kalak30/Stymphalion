using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton Pattern with ItemAssets
///     public static ItemAssets
/// Functions:
///     Awake()
/// </summary> 
/// 
/// <summary>
/// Creates list of sprite names in a singleton pattern
/// </summary>
public class ItemAssets : MonoBehaviour
{
    /// <summary>
    /// Static binding to get sprite from set
    /// </summary>
    public static ItemAssets Instance { get; private set; }

    /// <summary>
    /// Creates private set, allows 'get' to fetch sprites
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite m_sword_sprite;
    public Sprite m_health_potion_sprite;
    public Sprite m_gold_sprite;
    public Sprite m_medkit_sprite;
    public Sprite m_bow_sprite;
    public Sprite m_krotola_sprite;
    public Sprite m_quest_item_sprite;
    public Sprite m_hydra_blood_sprite;
}
