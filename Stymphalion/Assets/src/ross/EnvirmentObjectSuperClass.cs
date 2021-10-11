using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnvirmentObjectSuperClass : MonoBehaviour
{


    private PlayerInputActionMap playerActions;
    private bool interactPressed = false;

    public void Awake(){
        playerActions = new PlayerInputActionMap();
        playerActions.PlayerActionMap.Interact.started += interactIsPressed; 
        playerActions.PlayerActionMap.Interact.canceled += interactReleased;
        playerActions.PlayerActionMap.Interact.Enable();
    }

    public void interactIsPressed(InputAction.CallbackContext obj){
        interactPressed = true;
    }

    public void interactReleased(InputAction.CallbackContext obj){
        interactPressed = false;
    }

    void OnCollisionStay2D(){
        if(interactPressed){
            interactFunc();
        }
    }

    void OnTrigger2D(Collider2D obj){
        Debug.Log("BOOO");
    }

    public virtual void interactFunc(){
        Debug.Log("Generic Interacable object IDK");
       
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
