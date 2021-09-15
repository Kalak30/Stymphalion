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
    
    private test Tesst; // change name later if works
    private Rigidbody2D playRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake(){
        test = new Test();
        playRigidbody = GetComponent<Rigidbody2d>();
    }

    private void onEnable(){
        movement = test.Player.Movement;
        movementSpeed.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){

        forceDirection += movementSpeed.ReadValue<Vector2>() * movementSpeed;

        playRigidbody.AddForce(forceDirection, ForceMode2D.Impulse);
        forceDirection = Vector2.zero;

    }
}
