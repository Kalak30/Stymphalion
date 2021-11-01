/*
*
* Filename: EnvirmentObjectSuperClass.cs
* Developer: Ross Prestwich
* Purpose: Superclass for Enviroment Objects
* Implements a Bridge Pattern (Superclass is just and Interface)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Super Class For Enviroment Objects like NPC's and Traps
/// 
/// </summary>
public class EnvirmentObjectSuperClass : MonoBehaviour
{
//

    private PlayerInputActionMap m_player_actions;
    private bool m_interact_pressed = false;


    /// <summary>
    /// Awake Function
    /// Initalize all Variables
    /// Add actions to Action map
    /// </summary>
    public void Awake(){
        // initalize and enable action map
        m_player_actions = new PlayerInputActionMap();
        m_player_actions.PlayerActionMap.Interact.started += InteractIsPressed; 
        m_player_actions.PlayerActionMap.Interact.canceled += InteractReleased;
        m_player_actions.PlayerActionMap.Interact.Enable();
    }

/// <summary>
/// Function for checking if interact button is pressed
/// 
/// </summary>
/// <param name="obj"></param>
    public void InteractIsPressed(InputAction.CallbackContext obj){
        m_interact_pressed = true;
    }

/// <summary>
/// Function for resetting when interact button is released
/// </summary>
/// <param name="obj"></param>
    public void InteractReleased(InputAction.CallbackContext obj){
        m_interact_pressed = false;
    }


/// <summary>
/// Virtual Function for each NPC's Interaction
/// Dynamic Binding
/// 
/// </summary>
    public virtual void InteractFunc(){
        Debug.Log("Generic Interacable object IDK");
       
    }


/// <summary>
/// While Touching an enviroment object, interact with it
/// </summary>
    private void OnCollisionStay2D()
    {
        if(m_interact_pressed){
            InteractFunc();
        }
        m_interact_pressed = false;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        {
            if (m_interact_pressed)
            {
                InteractFunc();
            }
            m_interact_pressed = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

        // Update is called once per frame
        void Update()
        {

        }
}
