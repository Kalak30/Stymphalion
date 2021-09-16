using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    public float JumpForce = 5;
    public float movementSpeed = 5;

    private Vector2 forceDirection = Vector2.zero;
    
    private Test Tesst; // change name later if works
    private InputAction movement;
    private Rigidbody2D playRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake(){
        Debug.Log("awake\n");
        Tesst = new Test();
        playRigidbody = GetComponent<Rigidbody2D>();
        onEnable();
    }

    private void onEnable(){
        Debug.Log("endable\n");
        movement = Tesst.Player.Movement;
        movement.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        Debug.Log("gang gnag \n");
        Debug.Log("Mvement values::: " + movement.ReadValue<Vector2>() );
       // forceDirection += movement.ReadValue<Vector2>() * movementSpeed;
        playRigidbody.velocity = movement.ReadValue<Vector2>() * movementSpeed ;
        //forceDirection = Vector2.zero;

    }
}
