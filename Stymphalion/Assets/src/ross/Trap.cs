/*
*
* Filename: Trap.cs
* Developer: Ross Prestwich
* Purpose: Implementing Traps  ---- Subclass of EnvirmentObjectSuperClass.cs
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : EnvirmentObjectSuperClass
{

    public int m_damage = 1;
    private PlayerClass m_player;


    /// <summary>
    /// Damage Player
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay2D(Collider2D other){
        Debug.Log("TRIGGERED");

        // damage player
        if(other.name == "Player"){
            Debug.Log("obj.name == player");
            m_player = other.gameObject.GetComponent<PlayerClass>();
            m_player.m_health = m_player.m_health - m_damage;
        }
        

    }


}
