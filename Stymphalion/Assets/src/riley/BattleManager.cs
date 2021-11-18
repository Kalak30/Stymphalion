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
/// <item>Instance</item>
/// <item>m_enemyAction</item>
/// <item>m_player</item>
/// </list>
/// </summary>
public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance { get; private set; }
    PlayerClass m_player = PlayerClass.Instance;
    private EnemyAI m_enemyAction;


    /// <summary>
    /// Awake function checks if this is the only instance of the battle manager object
    /// </summary>
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
    ///Start function controls enemy actions
    ///Not part of my feature
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
    ///Function used in the HydraCav scene to switch to HydraBattle scene 
    ///</summary> 
    ///<param name =”name”> a parameter </param> 
    ///<returns> a return type </returns>
    public void InitializeBattle(){  

<<<<<<< Updated upstream
        player.m_new_scene_player_location = new Vector2(22, 3);
=======
        m_player.m_new_scene_player_location = new Vector2(21, 11);
>>>>>>> Stashed changes
        SceneManager.LoadScene("HydraBattle");
        m_enemyAction.EnemyMove();

    }


    /// <summary>
    /// Function called on the death of the hydra to return to the HydraCave
    /// </summary>
    public void EndBattle()
    {
<<<<<<< Updated upstream
            player.m_new_scene_player_location = new Vector2(22, 3);
=======
            m_player.m_new_scene_player_location = new Vector2(21, 11);
>>>>>>> Stashed changes
            SceneManager.LoadScene("HydraCave");
    }


}
