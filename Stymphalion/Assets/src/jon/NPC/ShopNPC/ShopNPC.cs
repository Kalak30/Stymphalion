/*
 * FileName: ShopNPC.cs
 * Developer: Jon Kopf
 * Purpose: Provides abstraction for a ShopNPC
 */
using TMPro;
using UnityEngine;
/// <summary>
/// Provides an abstraction for a ShopNPC
/// </summary>
public class ShopNPC : NPC
{
    GameObject m_shop_ui;
    bool m_open_shop = false;

    public void Awake()
    {
        m_shop_ui = GameObject.Find("Shop");
        m_shop_ui.SetActive(false);
    }

    public override void MoveTo(Vector2 pos)
    {

        // GetComponent<Transform>().position = pos;
    }

    /// <summary>
    /// Handles what happens when player is interacting with this intractable.
    /// Is automatically called
    /// </summary>
    public override void TouchingInteractable()
    {

        base.TouchingInteractable();
        //m_animator.Play("Base Layer.ShopNPCTalkAnim", 0, 0.5f);
        if (m_open_shop)
        {
            PlayerClass.Instance.OnEnable();
            m_open_shop = false;
            m_shop_ui.SetActive(false);
        }
        else
        {
            PlayerClass.Instance.OnDisable();
            m_open_shop = true;
            m_shop_ui.SetActive(true);
            GameObject m_ui = m_shop_ui.transform.Find("UI").gameObject;
            TextMeshProUGUI m_player_gold = m_ui.transform.Find("PlayerGold").GetComponent<TextMeshProUGUI>();

            m_player_gold.SetText(PlayerClass.Instance.CountGold(new Item { itemType = Item.ItemType.Gold, amount = 1 }).ToString());
        }

    }
}