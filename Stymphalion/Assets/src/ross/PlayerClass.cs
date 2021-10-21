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

public class PlayerClass : MonoBehaviour
{
    // public variables

    public float m_movement_speed;
    public Vector2 m_location;
    public int m_level = 0;
    public int m_xp = 0;
    public int m_health = 100;
    public bool m_on_fire = false;


    //Private Variables
    private Rigidbody2D m_player;

    private PlayerInputActionMap m_player_actions;
    private InputAction m_movement;
    private Inventory m_player_inventory;
    private Animator m_main_animator;

    private PlayerClass() { }

    private static readonly PlayerClass instance = new PlayerClass();

    public static PlayerClass GetPlayerClass(){
        return instance;
    }


    /// <summary>
    /// Intialize Everything Important in the program
    /// 
    /// </summary>
    private void Awake(){
        Debug.Log("awake\n");
        // add Inventory Object and Action map
        m_player_inventory = new Inventory();
        m_player_actions = new PlayerInputActionMap();
        // Get the rigid body from gameObject
        m_player = GetComponent<Rigidbody2D>();
     //   OnEnable(); // enable action map
        // add animator and set speed to 0
        m_main_animator = GetComponent<Animator>();
        m_main_animator.SetFloat("Speed", 0);
    }






/// <summary>
/// Enable Action map
/// Add Player Movement and Inventory function
/// </summary>
    private void OnEnable(){
        // add and enabple movement action map
        m_movement = m_player_actions.PlayerActionMap.Movement;
        m_movement.Enable();

        // add ineventory controls to action map
        m_player_actions.PlayerActionMap.Inventory.started += OpenInventory;
        m_player_actions.PlayerActionMap.Inventory.Enable();


        Debug.Log("Enabled");

    }//

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

/// <summary>
/// Function for moving Character
/// </summary>
    public virtual void Movement(){
        //  Debug.Log("Mvement values::: " + movement.ReadValue<Vector2>() );
        // playe values
        m_player.velocity = m_movement.ReadValue<Vector2>() * m_movement_speed;
        m_location = m_player.position;

        // set speed in animator to the player velocity
        m_main_animator.SetFloat("Speed", (float)Math.Sqrt((m_player.velocity.x * m_player.velocity.x) + (m_player.velocity.y * m_player.velocity.y)));

    }


    void Start()
    {
        //playerInventory = gameObject.AddComponent<Inventory>() as Inventory;
        //playerInventory = new Inventory();


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
/// Update Level when xp increases
/// </summary>
    private void LevelCheck()
    {
        while(m_xp >= 100)
        {
            m_xp = m_xp - 100;
            m_level = m_level + 1;
        }
    }

    /// <summary>
    /// Built in Unity function
    /// Runs every tick
    /// </summary>
    void FixedUpdate()
    {
        Movement();
        CheckHealth();
        LevelCheck();
    }
}
