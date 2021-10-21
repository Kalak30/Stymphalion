/*
 * Filename: BattleManager.cs
 * Developer: Riley Doyle
 * Purpose: Initiates and ends the battle scene and directs back to main scene
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item>m_enemyAction</item>
/// </list>
/// </summary>
public class BattleManager : MonoBehaviour
{
    private EnemyAI m_enemyAction;

    ///<summary>  
    ///A function 
    ///</summary> 
    ///<param name =”name”> a parameter </param> 
    ///<returns> a return type </returns> 
    void Start()
    {
        m_enemyAction = gameObject.AddComponent<EnemyAI>()as EnemyAI;
    }


    
    ///<summary>  
    ///A function 
    ///</summary> 
    ///<param name =”name”> a parameter </param> 
    ///<returns> a return type </returns>
    void Update()
    {
        
    }


    ///<summary>  
    ///A function 
    ///</summary> 
    ///<param name =”name”> a parameter </param> 
    ///<returns> a return type </returns>
    public void InitializeBattle(){

        Debug.Log("Battle Manager not yet set up");
        Debug.Log("Battle Manager will call function from AI shown below");
        m_enemyAction.EnemyMove();

    }


}
