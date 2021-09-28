using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClass : MonoBehaviour
{
    // public variables
    public Vector2 location;
    public int level = 0; 
    //public inventorySystem;
    public int health = 100;
    public bool onFire = false;

    private Rigidbody2D player;

    private PlayerInputActionMap playerActions;
    private InputAction movement;

    private void Awake(){
        playerActions = new PlayerInputActionMap();
        player = GetComponent<Rigidbody2D>();
        Debug.Log("awake");
       // OnEnable();
    }
    private void OnEnable(){
        movement = playerActions.PlayerActionMap.Movement;
        Debug.Log("Enabled");

    }
    private void Movement(){
        //change player location lmao
        player.velocity = movement.ReadValue<Vector2>();
        

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Mvement values::: " + playerActions.PlayerActionMap.Movement.ReadValue<Vector2>() );
        player.velocity = playerActions.PlayerActionMap.Movement.ReadValue<Vector2>();
    }
}
