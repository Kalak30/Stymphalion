using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using trevor


public class Inventory : MonoBehaviour
{
    private Inventory inventoryAction;
    // Start is called before the first frame update
    void Start()
    {
        inventoryAction = gameObject.AddComponent<Inventory>()as Inventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initializeBattle(){

        Debug.Log("Inventory not yet set up");
        Debug.Log("Inventory will call function from Inventory shown below");
        inventoryAction.InventoryCreation();

    }
}
