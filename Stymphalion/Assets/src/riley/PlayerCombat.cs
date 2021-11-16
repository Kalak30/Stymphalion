/*
 * Filename: PlayerCombat.cs
 * Developer: Riley Doyle
 * Purpose: Contains behavior of combat in relation to the player in boss battles
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item>m_attackPoint</item>
/// <item>m_enemyLayers</item>
/// <item>m_attackRange</item>
/// <item>m_attackDamage</item>
/// </list>
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    public Transform m_attackPoint;
    public LayerMask m_enemyLayers;
    public float m_attackRange = 0.5f;
    public int m_attackDamage = 10;
   



    ///<summary>  
    ///Update function 
    ///</summary> 
    ///<returns> void </returns>
    void Update()
    {
        if (Input.GetButtonDown("attack"))
        {
            Attack();
        }
    }


    ///<summary>  
    ///Alows the player to attack
    ///</summary> 
    ///<returns> void </returns>
    void Attack()
    {
        //Play attack animation
       

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(m_attackPoint.position, m_attackRange, m_enemyLayers);

        //Damage enemies in range
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(m_attackDamage);
        }
        
    }


    ///<summary>  
    ///Draws a wireframe for attack range for easy visualization
    ///</summary> 
    ///<returns> void </returns>
    void OnDrawGizmosSelected()
    {
        if (m_attackPoint == null)
            return;  

        Gizmos.DrawWireSphere(m_attackPoint.position, m_attackRange);
    }


}


