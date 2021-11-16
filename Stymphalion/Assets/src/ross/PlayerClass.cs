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
//using CodeMonkey.Utils;


/// <summary>
/// Main Player Class
/// Singleton Pattern
/// </summary>
public class PlayerClass
{

    public int m_health = 100;

    public int m_level = 0;

    public Vector2 m_location;
    public Vector2 m_new_scene_player_location = new Vector2(1.66f, 4.57f);


    // public variables
    public float m_movement_speed = 5;

    public bool m_on_fire = false;
    public int m_xp = 0;

    /// <summary>
    /// Return or create Singleton instance
    ///
    /// </summary>
    private static readonly Lazy<PlayerClass> lazy = new Lazy<PlayerClass>(() => new PlayerClass());

    //Private Variables //
    private bool m_facing_right = false;
    public void SetRight(){
        m_facing_right = false;
    }

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

    public void AddToInventory(Item item){
        m_player_inventory.AddItem(item, 1);
    }

    public void SetPlayerLocation(float x, float y){
        m_player_game_object.transform.position = new Vector2(x, y);
    }

    public Vector2 GetLocation()
    {
        return m_location;
    }
    //private PlayerClass m_instance = null;
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
        Debug.Log("awake\n");

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

        ItemWorld.SpawnItemWorld(new Vector3(4, 3), new Item { itemType = Item.ItemType.HealthPotion, amount = 2 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 3), new Item { itemType = Item.ItemType.Gold, amount = 11 });
        ItemWorld.SpawnItemWorld(new Vector3(2, 3), new Item { itemType = Item.ItemType.Gold, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(1, 3), new Item { itemType = Item.ItemType.Medkit, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 4), new Item { itemType = Item.ItemType.Bow, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 5), new Item { itemType = Item.ItemType.Krotola, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 6), new Item { itemType = Item.ItemType.HydraBlood, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 7), new Item { itemType = Item.ItemType.QuestItem, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 8), new Item { itemType = Item.ItemType.Sword, amount = 1 });

    }

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

        Debug.Log("Enabled");
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
        // Change function to what it's actually supposed to be when Kyle is ready
        m_ui_inventory.ToggleInventory();
        //  Debug.Log("Test");
    }

    private void OpenQuests(InputAction.CallbackContext obj)
    {
        GameObject.Find("QuestUI").GetComponent<QuestUI>().ToggleDisplay();
    }

    public void OnCollisionStay2D(Collision2D other){
        EnvirmentObjectSuperClass enviromentObj;
        enviromentObj = other.gameObject.GetComponent<EnvirmentObjectSuperClass>();
        if(m_interact_pressed){
            enviromentObj.InteractFunc();
        }
        m_interact_pressed = false;
    }

    public void OnTriggerStay2D(Collider2D other){
        EnvirmentObjectSuperClass enviromentObj;
        enviromentObj = other.gameObject.GetComponent<EnvirmentObjectSuperClass>();
        if(m_interact_pressed){
            enviromentObj.InteractFunc();
        }
        m_interact_pressed = false;
    }
    
    public void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            // Touching item
            m_player_inventory.AddItem(itemWorld.GetItem(), 1);
            itemWorld.DestroySelf();
        }
    }
    
    public void InteractIsPressed(InputAction.CallbackContext obj){
        m_interact_pressed = true;
    } 

    public void InteractReleased(InputAction.CallbackContext obj){
        m_interact_pressed = false;
    }
}