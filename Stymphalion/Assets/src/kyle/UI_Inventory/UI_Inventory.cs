using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public GameObject m_ui_inventory;

    private void Awake()
    {

        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        itemSlotContainer.gameObject.SetActive(false);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }
    private void RefreshInventoryItems()
    {
        Debug.Log("LOLs");
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x++;
            Debug.Log("Pls");
            if ( x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
    public void ToggleInventory()
    {
        GameObject temp = GameObject.Find("UI_Inventory");
        m_ui_inventory = temp.transform.Find("Inventory").gameObject;
        m_ui_inventory.SetActive(!m_ui_inventory.activeSelf);

        itemSlotContainer.gameObject.SetActive(!itemSlotContainer.gameObject.activeSelf);

        if (m_ui_inventory.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
