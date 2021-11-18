using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;  


public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private PlayerClass m_player; 
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate; 
    public GameObject m_ui_inventory; 


    private void Awake()
    {

        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        itemSlotContainer.gameObject.SetActive(false);
    }
    public void SetPlayer(PlayerClass m_player)
    {
        this.m_player = m_player;
    }
    
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        { 
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        float x = -14.5f;
        float y = 3.5f; 
        int x_count = 0;
        float itemSlotCellSize = 20f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                // Use item
                inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                // Drop item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(m_player.GetLocation(), duplicateItem);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amount").GetComponent<TextMeshProUGUI>(); 
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }


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
    public void ToggleInventory()
    {
        GameObject temp = GameObject.Find("UI_Inventory");
        m_ui_inventory = temp.transform.Find("Inventory").gameObject;
        m_ui_inventory.SetActive(!m_ui_inventory.activeSelf);

        itemSlotContainer.gameObject.SetActive(!itemSlotContainer.gameObject.activeSelf);
        
    }
}
