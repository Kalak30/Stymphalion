using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClass : MonoBehaviour
{
    // public variables
    
    public int movementSpeed = 5;
    public Vector2 location;
    public int level = 0; 
    //public inventorySystem;
    public int health = 100;
    public bool onFire = false;

    private Rigidbody2D player;

    private PlayerInputActionMap playerActions;
    private InputAction movement;
    

    /*
    private void OnCollisionStay2D(Collision2D collisionInfo){
        // Debug.Log("Trash");
        EnvirmentObjectSuperClass otherScript = collisionInfo.gameObject.GetComponent<EnvirmentObjectSuperClass>();

        if(true){
            otherScript.interact();
        }
    }
    */
  private void Awake(){
        Debug.Log("awake\n");
        playerActions = new PlayerInputActionMap();
        player = GetComponent<Rigidbody2D>();
        OnEnable();
    }
    private void OnEnable(){
        movement = playerActions.PlayerActionMap.Movement;
        movement.Enable();
        Debug.Log("Enabled");

    }
    private void Movement(){
      //  Debug.Log("Mvement values::: " + movement.ReadValue<Vector2>() );
        player.velocity = movement.ReadValue<Vector2>() * 5;
        location = player.position;
        

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
}