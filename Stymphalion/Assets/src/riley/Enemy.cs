/*
 * Filename: Enemy.cs
 * Developer: Riley Doyle
 * Purpose: Contains behaviors asociated with enemy objects
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item>m_maxHealth</item>
/// <item>m_currentHealth</item>
/// </list>
/// </summary>
public class Enemy : MonoBehaviour
{
    public int m_maxHealth = 100;
    public int m_currentHealth;


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
        Debug.Log("Enemy Died");

        //Make Enemy disappear
        GameObject objectToDisappear = GameObject.Find("Boss");
        objectToDisappear.GetComponent<Renderer>().enabled = false;

        //Die animation

        //Disable the enemy
    }

}