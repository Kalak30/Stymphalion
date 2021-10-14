using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
//s
public class PlayerClass : MonoBehaviour
{
    // public variables
    
    public float movementSpeed;
    public Vector2 location;
    public int level = 0; 
    public int xp = 0;
   
    public int health = 100;
    public bool onFire = false;


    //Private Variables
    private Rigidbody2D player;

    private PlayerInputActionMap playerActions;
    private InputAction movement;
    private Inventory playerInventory;
    private Animator MainAnimator;


    // Should I be Doing all this in Start or Awake?? Idk but it works here. 
  private void Awake(){
        Debug.Log("awake\n");
        // add Inventory Object and Action map
        playerInventory = new Inventory();
        playerActions = new PlayerInputActionMap();
        // Get the rigid body from gameObject
        player = GetComponent<Rigidbody2D>();
        OnEnable(); // enable action map
        // add animator and set speed to 0
        MainAnimator =  GetComponent<Animator>(); 
        MainAnimator.SetFloat("Speed", 0);
    }

    private void OnEnable(){
        // add and enabple movement action map
        movement = playerActions.PlayerActionMap.Movement;
        movement.Enable();

        // add ineventory controls to action map
        playerActions.PlayerActionMap.Inventory.performed += inventoryX; 
        playerActions.PlayerActionMap.Inventory.Enable();


        Debug.Log("Enabled");

    }//

    //open Inventory
    private void inventoryX(InputAction.CallbackContext obj){
        // Change function to what it's actually supposed to be when Kyle is ready
        playerInventory.InventoryCreation();
      //  Debug.Log("Test");
    }

    // Make virutal for testing purposes
    public virtual void Movement(){
      //  Debug.Log("Mvement values::: " + movement.ReadValue<Vector2>() );
      // playe values
        player.velocity = movement.ReadValue<Vector2>() * movementSpeed;
        location = player.position;
        
        // set speed in animator to the player velocity
        MainAnimator.SetFloat("Speed", (float) Math.Sqrt((player.velocity.x * player.velocity.x) + (player.velocity.y * player.velocity.y))) ;

    }


    // Start is called before the first frame update
    void Start()
    {
        //playerInventory = gameObject.AddComponent<Inventory>() as Inventory;
        // should be playerInventory = new Inventory;

    }

    // make sure player isn't going out of bounds
    // Should Probably use a function to update health instead of making it a public variable
    private void checkHealth(){
        if (health > 100){
            health = 100;
        }
        else if (health < 0){
            health = 0;
        }
    }


    // Update Level
    private void levelCheck(){
        while(xp >= 100){
            xp = xp - 100;
            level = level + 1;
        }
    }

    // Run all functions as needed
    // levelCheck and checkHealth probably shouldnt be here
    void FixedUpdate()
    {
        Movement();
        checkHealth();
        levelCheck();
    }
}
