using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;  

/// <summary>
/// All the code for the inventory graphic displayed by PlayerClass
/// </summary>
public class UI_Inventory : MonoBehaviour
{
    private Inventory m_inventory;
    private PlayerClass m_player; 
    private Transform m_item_slot_container;
    private Transform m_item_slot_template; 
    public GameObject m_ui_inventory; 

    /// <summary>
    /// Finds the game objects for the UI graphic
    ///     Sets the inventory off as default
    /// </summary>
    private void Awake()
    {

        m_item_slot_container = transform.Find("itemSlotContainer");
        m_item_slot_template = m_item_slot_container.Find("itemSlotTemplate");
        m_item_slot_container.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets player
    /// </summary>
    /// <param name="m_player"></param>
    public void SetPlayer(PlayerClass m_player)
    {
        this.m_player = m_player;
    }
    
    /// <summary>
    /// Sets inventory and refreshes it when the list changes
    ///     Adds changes to inventory
    /// </summary>
    /// <param name="inventory"></param>
    public void SetInventory(Inventory inventory)
    {
        this.m_inventory = inventory;
        inventory.m_on_item_list_changed += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }
    
    /// <summary>
    /// Refreshes inventory when items change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    /// <summary>
    /// Creates sizes for containers
    ///     Allows for right and left click on inventory
    ///     Transforms all items to have sprites, 
    ///         their own position in the Inventory UI, 
    ///         sets text for amount of objects that are stacked, 
    ///         
    /// </summary>
    private void RefreshInventoryItems()
    {
        foreach (Transform child in m_item_slot_container)
        { 
            if (child == m_item_slot_template) continue;
            Destroy(child.gameObject);
        }
        float x = -14.5f;
        float y = 3.5f; 
        int x_count = 0;
        float itemSlotCellSize = 20f;
        foreach (Item item in m_inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(m_item_slot_template, m_item_slot_container).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                // Use item
                m_inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                // Drop item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                m_inventory.RemoveItem(item);
                ItemWorld.DropItem(m_player.GetLocation(), duplicateItem);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amount").GetComponent<TextMeshProUGUI>(); 
            // amount only shows when more than 1 item exists
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            // Changes where the item is present in inventory for each item
            x += 5.75f;
            x_count++;
            if ( x_count % 6 == 0 )
            {
                x = -14.5f;
                if (x_count % 6 == 0)
                {
                    y = y - 6.05f;
                }
            }
        }
    }

    /// <summary>
    /// Toggles the inventory on and off
    ///     Player class access function with the key 'i' is pressed
    /// </summary>
    public void ToggleInventory()
    {
        GameObject temp = GameObject.Find("UI_Inventory");
        m_ui_inventory = temp.transform.Find("Inventory").gameObject;
        m_ui_inventory.SetActive(!m_ui_inventory.activeSelf);

        m_item_slot_container.gameObject.SetActive(!m_item_slot_container.gameObject.activeSelf);
        
    }
}
