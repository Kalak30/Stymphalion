/*
 * FileName: ShopNPC.cs
 * Developer: Jon Kopf
 * Purpose: Provides abstraction for a ShopNPC
 */
using UnityEngine;
/// <summary>
/// Provides an abstraction for a ShopNPC
/// </summary>
public class ShopNPC : NPC
{
    GameObject shop_ui;

    public void Awake()
    {
        shop_ui = GameObject.Find("Shop");
        shop_ui.SetActive(false);
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
        m_animator.Play("Base Layer.ShopNPCTalkAnim", 0, 0.5f);
        shop_ui.SetActive(true);
        
    }
}