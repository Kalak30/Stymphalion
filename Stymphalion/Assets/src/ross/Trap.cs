using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : EnvirmentObjectSuperClass
{

    public int damage = 1;
    private PlayerClass player;
    void OnTriggerStay2D(Collider2D other){
        Debug.Log("TRIGGERED");
        if(other.name == "Player"){
            Debug.Log("obj.name == player");
            player = other.gameObject.GetComponent<PlayerClass>();
            player.health = player.health - damage;
        }
        

    }


}
