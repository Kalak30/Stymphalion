/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    public float JumpForce = 5;
    public float movementSpeed = 5;

    private Vector2 forceDirection = Vector2.zero;
    
    private PlayerActionMap ActionMap; // change name later if works
    private InputAction movement;
    private Rigidbody2D playRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake(){
        Debug.Log("awake\n");
        ActionMap = new PlayerActionMap();
        playRigidbody = GetComponent<Rigidbody2D>();
        onEnable();
    }

    private void onEnable(){
        Debug.Log("endable\n");
        movement = ActionMap.Player.Movement;
      //  movement.Enable();

        ActionMap.Player.Interact.performed += interactFunc;
        ActionMap.Player.Interact.Enable();

        Debug.Log("endable part 2: battle tendencies");

    }

    private void interactFunc(InputAction.CallbackContext obj){
        Debug.Log("Interact Code Runs I guess lmao\n");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        //Debug.Log("gang gnag \n");
        //Debug.Log("Mvement values::: " + movement.ReadValue<Vector2>() );
       // forceDirection += movement.ReadValue<Vector2>() * movementSpeed;
        playRigidbody.velocity = movement.ReadValue<Vector2>() * movementSpeed ;
        //forceDirection = Vector2.zero;

    }
}
*/