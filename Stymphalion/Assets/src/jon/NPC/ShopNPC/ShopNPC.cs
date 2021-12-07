/*
 * FileName: ShopNPC.cs
 * Developer: Jon Kopf
 * Purpose: Provides abstraction for a ShopNPC
 */
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Provides an abstraction for a ShopNPC
/// </summary>
public class ShopNPC : NPC
{
    GameObject m_shop_ui;
    bool m_open_shop = false;
    private PlayerInputActionMap m_input_actions;

    /// <summary>
    /// Find the shop ui and close it
    /// </summary>
    public void Awake()
    {
        m_input_actions = PlayerClass.Instance.m_player_actions;
        m_shop_ui = GameObject.Find("Shop");
        m_shop_ui.SetActive(false);
    }

    /// <summary>
    /// Move to a given location
    /// </summary>
    /// <param name="pos"></param>
    public override void MoveTo(Vector2 pos)
    {

        // GetComponent<Transform>().position = pos;
    }

    /// <summary>
    /// Make the interact button work
    /// </summary>
    /// <param name="obj"></param>
    public void InteractIsPressed(InputAction.CallbackContext obj)
    {
        
        if (m_open_shop)
        {
            PlayerClass.Instance.OnEnable();
            m_open_shop = false;
            m_shop_ui.SetActive(false);

        }

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
            Debug.LogError("Close Shop");
            PlayerClass.Instance.OnEnable();
            m_open_shop = false;
            m_shop_ui.SetActive(false);
        }
        else
        {
            Debug.LogError("Open shop");

            PlayerClass.Instance.OnDisable();
            m_open_shop = true;
            m_shop_ui.SetActive(true);
            GameObject m_ui = m_shop_ui.transform.Find("UI").gameObject;
            TextMeshProUGUI m_player_gold = m_ui.transform.Find("PlayerGold").GetComponent<TextMeshProUGUI>();

            m_player_gold.SetText(PlayerClass.Instance.CountGold(new Item { itemType = Item.ItemType.Gold, amount = 1 }).ToString());
        }

    }
}