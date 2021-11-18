/*
 * Filename: Enemy.cs
 * Developer: Riley Doyle
 * Purpose: Contains behaviors asociated with enemy objects
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item>hydraDeath</item>
/// <item>m_maxHealth</item>
/// <item>m_currentHealth</item>
/// <item>animator</item>
/// <item>player</item>
/// </list>
/// </summary>
public class Enemy : MonoBehaviour
{
    public static event Action hydraDeath;
    public int m_maxHealth = 100;
    public int m_currentHealth;
    public Animator animator;
    PlayerClass player = PlayerClass.Instance;  

    ///<summary>  
    ///Start function sets current healt to max health 
    ///</summary> 
    ///<returns> void </returns>
    void Start()
    {
        m_currentHealth = m_maxHealth;

    }


    ///<summary>  
     ///Allows Enemy to take damage and die when health is at or below zero
    ///returns true if hydra dies
    ///</summary> 
    ///<param name =”damage”> int </param> 
    ///<returns> bool </returns>
    public bool TakeDamage(int damage)
    {
        m_currentHealth -= damage;

        if (m_currentHealth <= 0)
        {
            Die();
            return true;
        }
        return false;
    }


    ///<summary>
    ///Sends player back to HydraCave scene
    ///Triggers event when enemy dies to send to observer
    ///</summary> 
    ///<returns> void </returns>
    void Die()
    {
        hydraDeath?.Invoke();
        Debug.Log("Enemy Died");

        //Make Enemy disappear
        gameObject.SetActive(false);

        player.m_new_scene_player_location = new Vector2(22, 3);
        SceneManager.LoadScene("HydraCave");
        /*
        GameObject objectToDisappear = GameObject.Find("Enemy");
        objectToDisappear.GetComponent<Renderer>().enabled = false;
        */
    }

}