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
    public int m_health = 100;

    public int m_level = 0;

    public Vector2 m_location;

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
    private Inventory m_player_inventory;

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
    public void InitVariables()
    {

        Debug.Log("awake\n");

        // add Inventory Object and Action map
        m_player_inventory = new Inventory();
        m_player_actions = new PlayerInputActionMap();

        // Get the rigid body from gameObject
        m_player = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        // add animator and set speed to 0
        m_main_animator = GameObject.Find("Player").GetComponent<Animator>();
        m_main_animator.SetFloat("Speed", 0);
        m_player_game_object = GameObject.Find("Player");
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
        Debug.Log("160");
        // Change function to what it's actually supposed to be when Kyle is ready
        m_player_inventory.ToggleInventory();
        //  Debug.Log("Test");
    }

    private void OpenQuests(InputAction.CallbackContext ogj)
    {
        GameObject.Find("QuestUI").GetComponent<QuestUI>().ToggleDisplay();
    }
}