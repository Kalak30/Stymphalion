/*
 * Filename: BattleManager.cs
 * Developer: Riley Doyle
 * Purpose: Initiates and ends the battle scene and directs back to main scene
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



/// <summary>
/// Singleton design pattern
/// Member Variables
/// <list type = "bullet">
/// <item>m_enemyAction</item>
/// </list>
/// </summary>
public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance { get; private set; }
    PlayerClass player = PlayerClass.Instance;
    private EnemyAI m_enemyAction;


    void Awake()
    {
        if (Instance == null ) //if the code is running for the first time
        {
            Instance = this; // set it to contain the current instance
            DontDestroyOnLoad(gameObject);
        }
        else //if another instance already exists destroy the previous instances
        {
            Destroy(gameObject);
        }
    }
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

        player.m_new_scene_player_location = new Vector2(21, 11);
        SceneManager.LoadScene("HydraBattle");
        m_enemyAction.EnemyMove();

    }


    public void EndBattle()
    {
            player.m_new_scene_player_location = new Vector2(21, 11);
            SceneManager.LoadScene("HydraCave");
    }


}
