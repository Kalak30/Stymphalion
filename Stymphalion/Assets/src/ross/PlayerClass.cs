/*
*
* Filename: PayerClass.cs
* Developer: Ross Prestwich
* Purpose: Implementing Player movements and data
*/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Main Player Class
/// Singleton Pattern
/// </summary>
public class PlayerClass
{

    // Public Variables
    public int m_health = 100;
    public int m_level = 0;
    public Vector2 m_location;
    public Vector2 m_new_scene_player_location = new Vector2(1.66f, 4.57f);
    public float m_movement_speed = 5;
    public bool m_on_fire = false;
    public int m_xp = 0;
    public bool m_is_inventory_open = false;




    /// <summary>
    /// Return or create Singleton instance
    ///
    /// </summary>
    private static readonly Lazy<PlayerClass> lazy = new Lazy<PlayerClass>(() => new PlayerClass());



    //Private Variables //
    private bool m_facing_right = false;
    private Animator m_main_animator;
    private InputAction m_movement;
    private Rigidbody2D m_player;
    private PlayerInputActionMap m_player_actions;
    private GameObject m_player_game_object;
    private UI_Inventory m_ui_inventory;
    private Inventory m_player_inventory;
    private bool m_interact_pressed;

    /// <summary>
    /// Contructor
    /// </summary>
    private PlayerClass() { }

    /// <summary>
    /// Get Singleton Instance
    /// </summary>
    public static PlayerClass Instance { get { return lazy.Value; } }

    /// <summary>
    /// Built in Unity function
    /// Runs every tick
    /// </summary>
    public void FixedUpdate()
    {
        Movement();
        CheckHealth();
        LevelCheck();
    }

    /// <summary>
    /// Set Player Location to right
    /// </summary>
    public void SetRight(){
        m_facing_right = false;
    }

    /// <summary>
    /// Add item to Player's Inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddToInventory(Item item){
        m_player_inventory.AddItem(item, 1);
    }

    /// <summary>
    /// Remove an Item From Player's Inventory
    /// </summary>
    /// <param name="item"></param>
    public void RemoveFromInventory(Item item)
    {
        m_player_inventory.RemoveItem(item);
    }

    /// <summary>
    /// Return ammount of gold in Player Inventory
    /// </summary>
    /// <param name="m_gold"></param>
    /// <returns></returns>
    public int CountGold(Item m_gold)
    {
        int m_gold_amount = 0;
        foreach (Item item in m_player_inventory.GetItemList())
        {
            if (m_gold.itemType == item.itemType)
            {
                m_gold_amount = item.amount;
            }
        }
        return m_gold_amount;
    }
    /// <summary>
    /// Return total items in Player Inventory
    /// </summary>
    /// <returns></returns>
    public int CountItemsInInventory()
    {
        return m_player_inventory.ItemCount();
    }

    /// <summary>
    /// Set player Location on Load
    /// Not my favorite why of doing it
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPlayerLocation(float x, float y){
        m_player_game_object.transform.position = new Vector2(x, y);
    }

    /// <summary>
    /// Return Player Location
    /// </summary>
    /// <returns></returns>
    public Vector2 GetLocation()
    {
        return m_player.position;
    }

    /// <summary>
    /// Will be used to properly get the singelton class
    /// Work In progress
    /// </summary>
    /// <returns></returns>
    public PlayerClass GetPlayerClass()
    {
        return Instance;
    }


    /// <summary>
    /// Intialize Everything Important in the program
    /// previously MonoBehavior Awake
    /// </summary>
    public void InitVariables(UI_Inventory uiInventory)
    {
        // add Inventory Object and Action map
        if (m_player_inventory == null)
        {
            
            m_player_inventory = new Inventory(UseItem);
            m_ui_inventory = uiInventory;
            m_ui_inventory.SetPlayer(this);
            uiInventory.SetInventory(m_player_inventory);
           
        }
        //m_ui_inventory.SetPlayer(this);
        m_player_actions = new PlayerInputActionMap();

        // Get the rigid body from gameObject
        m_player = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        // add animator and set speed to 0
        m_main_animator = GameObject.Find("Player").GetComponent<Animator>();
        m_main_animator.SetFloat("Speed", 0);
        m_player_game_object = GameObject.Find("Player");
        SetPlayerLocation(m_new_scene_player_location.x, m_new_scene_player_location.y);

     
    }

    /// <summary>
    /// Do affect when Item is used
    /// </summary>
    /// <param name="item"></param>
    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                m_player_inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                m_health += 10;
                break;
            case Item.ItemType.Medkit:
                m_player_inventory.RemoveItem(new Item { itemType = Item.ItemType.Medkit, amount = 1 });
                m_health += 100;
                break;
        }
    }

    /// <summary>
    /// Disable movements
    /// </summary>
    public void OnDisable()
    {
        m_movement.Disable();

        m_player_actions.PlayerActionMap.Inventory.started -= OpenInventory;
        m_player_actions.PlayerActionMap.Inventory.Disable();

        m_player_actions.PlayerActionMap.Quest.started -= OpenQuests;
        m_player_actions.PlayerActionMap.Quest.Disable();
    }

    /// <summary>
    /// Enable Action map
    /// Add Player Movement and Inventory function
    /// </summary>
    public void OnEnable()
    {
        // add and enabple movement action map
        m_movement = m_player_actions.PlayerActionMap.Movement;
        m_movement.Enable();

        // add ineventory controls to action map
        m_player_actions.PlayerActionMap.Inventory.started += OpenInventory;
        m_player_actions.PlayerActionMap.Inventory.Enable();

        m_player_actions.PlayerActionMap.Quest.started += OpenQuests;
        m_player_actions.PlayerActionMap.Quest.Enable();

        m_player_actions.PlayerActionMap.Interact.started += InteractIsPressed; 
        //m_player_actions.PlayerActionMap.Interact.canceled += InteractReleased;
        m_player_actions.PlayerActionMap.Interact.Enable();
    }

    /// <summary>
    /// Make Sure Health does not become negative
    /// The better way to do this is make everything go through a function to change
    /// </summary>
    private void CheckHealth()
    {
        if (m_health > 100)
        {
            m_health = 100;
        }
        else if (m_health < 0)
        {
            m_health = 0;
        }
    }

    /// <summary>
    /// Flip The Player Model when direction Changes
    /// </summary>
    private void Flip()
    {
        Vector3 flipper = m_player_game_object.transform.localScale;
        flipper.x *= -1;
        m_player_game_object.transform.localScale = flipper;
        m_facing_right = !m_facing_right;
    }

    /// <summary>
    /// Update Level when xp increases
    /// </summary>
    private void LevelCheck()
    {
        while (m_xp >= 100)
        {
            m_xp = m_xp - 100;
            m_level = m_level + 1;
        }
    }

    /// <summary>
    /// Function for moving Character
    /// </summary>
    private void Movement()
    {
        //  Debug.Log("Mvement values::: " + movement.ReadValue<Vector2>() );
        // playe values
        m_player.velocity = m_movement.ReadValue<Vector2>() * m_movement_speed;
        m_location = m_player.position;
        if (m_player.velocity.x < 0 && !m_facing_right)
        {
            Flip();
        }
        else if (m_player.velocity.x > 0 && m_facing_right)
        {
            Flip();
        }

        // set speed in animator to the player velocity
        m_main_animator.SetFloat("Speed", (float)Math.Sqrt((m_player.velocity.x * m_player.velocity.x) + (m_player.velocity.y * m_player.velocity.y)));
    }

    /// <summary>
    /// Open Invetory
    /// </summary>
    /// <param name="obj"></param>
    private void OpenInventory(InputAction.CallbackContext obj)
    {
        m_ui_inventory.ToggleInventory();
        if (m_is_inventory_open == false)
        {
            m_movement.Disable();
            m_is_inventory_open = true;
        }
        else
        {
            m_movement.Enable();
            m_is_inventory_open = false;
        }
        
    }

    /// <summary>
    /// Turn on the quest menu
    /// </summary>
    /// <param name="obj"></param>
    private void OpenQuests(InputAction.CallbackContext obj)
    {
        GameObject.Find("QuestUI").GetComponent<QuestUI>().ToggleDisplay();
    }

    /// <summary>
    /// Allow Interaction with Enviroment objects
    /// Dynamic Binding lets goo
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionStay2D(Collision2D other){
        EnvirmentObjectSuperClass enviromentObj;
        enviromentObj = other.gameObject.GetComponent<EnvirmentObjectSuperClass>();
        if(m_interact_pressed){
            enviromentObj.InteractFunc();
        }
        m_interact_pressed = false;
    }

    /// <summary>
    /// Allow Interaction with Enviroment Objects
    /// Dynamic Binding lets gooo
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerStay2D(Collider2D other){
        EnvirmentObjectSuperClass enviromentObj;
        enviromentObj = other.gameObject.GetComponent<EnvirmentObjectSuperClass>();
        if(m_interact_pressed){
            enviromentObj.InteractFunc();
        }
        m_interact_pressed = false;
    }
    

    /// <summary>
    /// Add Item to Inventory
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "QuestTrigger")
        {
            QuestManager.GetQuestManager().Trigger(collider);
        }

        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            // Touching item
            m_player_inventory.AddItem(itemWorld.GetItem(), 1);
            if (m_player_inventory.ItemCount() < 18)
            {
                itemWorld.DestroySelf();
            }
        }
    }
    
    /// <summary>
    /// Make the interact button work
    /// </summary>
    /// <param name="obj"></param>
    public void InteractIsPressed(InputAction.CallbackContext obj){
        m_interact_pressed = true;
    } 

    /// <summary>
    /// Make the ineract button work
    /// </summary>
    /// <param name="obj"></param>
    public void InteractReleased(InputAction.CallbackContext obj){
        m_interact_pressed = false;
    }
}