using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//s
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


    // Should I be Doing all this in Start or Awake?? Idk but it works here. 
    private void Awake(){
        Debug.Log("awake\n");
        // add Inventory Object and Action map
        m_player_inventory = new Inventory();
        m_player_actions = new PlayerInputActionMap();
        // Get the rigid body from gameObject
        m_player = GetComponent<Rigidbody2D>();
        OnEnable(); // enable action map
        // add animator and set speed to 0
        m_main_animator = GetComponent<Animator>();
        m_main_animator.SetFloat("Speed", 0);
    }

    private void OnEnable(){
        // add and enabple movement action map
        m_movement = m_player_actions.PlayerActionMap.Movement;
        m_movement.Enable();

        // add ineventory controls to action map
        m_player_actions.PlayerActionMap.Inventory.performed += OpenInventory;
        m_player_actions.PlayerActionMap.Inventory.Enable();


        Debug.Log("Enabled");

    }//

    //open Inventory
    private void OpenInventory(InputAction.CallbackContext obj)
    {
        // Change function to what it's actually supposed to be when Kyle is ready
        m_player_inventory.InventoryCreation();
        //  Debug.Log("Test");
    }

    // Make virutal for testing purposes
    public virtual void Movement(){
        //  Debug.Log("Mvement values::: " + movement.ReadValue<Vector2>() );
        // playe values
        m_player.velocity = m_movement.ReadValue<Vector2>() * m_movement_speed;
        m_location = m_player.position;

        // set speed in animator to the player velocity
        m_main_animator.SetFloat("Speed", (float)Math.Sqrt((m_player.velocity.x * m_player.velocity.x) + (m_player.velocity.y * m_player.velocity.y)));

    }


    // Start is called before the first frame update
    void Start()
    {
        //playerInventory = gameObject.AddComponent<Inventory>() as Inventory;
        //playerInventory = new Inventory();


    }

    // make sure player isn't going out of bounds
    // Should Probably use a function to update health instead of making it a public variable
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


    // Update Level
    private void LevelCheck()
    {
        while(m_xp >= 100)
        {
            m_xp = m_xp - 100;
            m_level = m_level + 1;
        }
    }

    // Run all functions as needed
    // levelCheck and checkHealth probably shouldnt be here
    void FixedUpdate()
    {
        Movement();
        CheckHealth();
        LevelCheck();
    }
}
