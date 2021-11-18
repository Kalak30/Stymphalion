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
/// <item>m_maxHealth</item>
/// <item>m_currentHealth</item>
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
    ///Start function 
    ///</summary> 
    ///<returns> void </returns>
    void Start()
    {
        m_currentHealth = m_maxHealth;

    }


    ///<summary>  
    ///Allows Enemy to take damage
    ///</summary> 
    ///<param name =”damage”> int </param> 
    ///<returns> bool </returns>
    public bool TakeDamage(int damage)
    {
        m_currentHealth -= damage;

        //Play hurt animation

        if (m_currentHealth <= 0)
        {
            Die();
            return true;
        }
        return false;
    }


    ///<summary>  
    ///Action when Enemy reeaches zero health
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