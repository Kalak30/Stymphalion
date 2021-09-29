using System;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour {
    Vector2 position;
    String name;


    public void touchingInteractable(){
        Debug.Log("=======================\n
                   --------- NPC ---------\n
                   touchingInteractable()\n
                   =======================\n")
    }
}