/*
 * Filename: Player.cs
 * Developer: Riley Doyle
 * Purpose: Contains all behaviors associated with the player class in the boss battle scene
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
public class Player : MonoBehaviour
{

    public static int m_maxHealth;
    int m_currentHealth;

    
    ///<summary>  
    ///set current health to max health on start
    ///</summary> 
    ///<returns> void </returns>
    void Start()
    {
        int m_currentHealth = m_maxHealth;

    }

    
    ///<summary>  
    ///Gives player  health
    ///</summary> 
    ///<param name =”health”> int </param> 
    ///<returns> void </returns>
    public void GainHealth(int health)
    {
        m_currentHealth += health;
        Debug.Log("After healing: " + m_currentHealth); 

        if (m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
    }


    ///<summary>  
    ///Allows player to take damage when getting hit 
    ///</summary> 
    ///<param name =”damage”> int </param> 
    ///<returns> void </returns>
    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        Debug.Log("After Damage: " + m_currentHealth);

        if (m_currentHealth <= 0)
        {
            int after = 0 - m_currentHealth;
            Debug.Log("Health is " + after + " below boundry");
            m_currentHealth = 0;
            Die();
        }
    }


    ///<summary>  
    ///Action when player dies
    ///</summary> 
    ///<returns> void </returns>
    void Die()
    {
        Debug.Log("Player Died");

        //Make Enemy disappear
        GameObject objectToDisappear = GameObject.Find("Player");
        objectToDisappear.GetComponent<Renderer>().enabled = false;

    }


    ///<summary>  
    ///Returns player's current health
    ///</summary> 
    ///<returns> int </returns>
    public int GetCurrentHealth()
    {
        return m_currentHealth;
    }


    ///<summary>  
    ///Returns player's max health 
    ///</summary> 
    ///<returns> int </returns>
    public int GetMaxHealth()
    {
        return m_maxHealth;
    }


}

