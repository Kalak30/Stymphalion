/*
 * Filename: PlayerAI.cs
 * Developer: Trevor McGeary
 * Purpose: Contains behavior for the player AI, and returns the AI move to the battle system.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public float max_Health = 200;
    public float current_Health;
    public float health_Percentage;
    public int behavior;
    public float enemy_Health;
    public float enemy_Health_Percentage;

    //public EnemyAIDemo enemy_Data;
    //public BattleSystem battleSystem;
    public UnitDataClass[] test_Array;
    private BattleSystem battle_System;
    
    // Start is called before the first frame update
    void Start()
    {
        battle_System = FindObjectOfType<BattleSystem>();
        //current_Health = max_Health;
        //enemy_Health = enemy_Data.current_Health;
        //enemy_Health_Percentage = enemy_Health / enemy_Data.max_Health;
    }

    private void Awake()
    {
        battle_System = FindObjectOfType<BattleSystem>();
        //behavior = battle_System.unitArray[1].behavior;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns the number corresponding to which attack the player AI will use.
    /// </summary>
    /// <returns></returns>
    public int PlayerMove()
    {
        //behavior = 1;
        //Debug.Log(battleSystem.unitArray.Length);
        behavior = battle_System.unitArray[1].behavior;
        enemy_Health = battle_System.unitArray[0].current_Health;
        enemy_Health_Percentage = battle_System.unitArray[0].current_Health / battle_System.unitArray[0].max_Health;
        enemy_Health_Percentage = enemy_Health / battle_System.unitArray[0].max_Health;
        current_Health = battle_System.unitArray[1].current_Health;
        health_Percentage = current_Health / max_Health;
        //behavior = 1;
        if(behavior == 1)
        {
            
            if (enemy_Health_Percentage >= .75 && health_Percentage >= .50)
            {
                return 1;
            }
            else if (enemy_Health_Percentage >= .75 && health_Percentage < .50)
            {
                return 0;
            }
            else if (enemy_Health_Percentage <= .75 && enemy_Health_Percentage >=.50 && health_Percentage >= .50)
            {
                return 3;
            }
            else if (enemy_Health_Percentage <= .75 && enemy_Health_Percentage >= .50 && health_Percentage < .50)
            {
                return 3;
            }
            else if (enemy_Health_Percentage <= .50 && enemy_Health_Percentage >= .30 && health_Percentage >= .50)
            {
                return 4;
            }
            else if (enemy_Health_Percentage <= .50 && enemy_Health_Percentage >= .30 && health_Percentage < .50)
            {
                return 2;
            }
            else if (enemy_Health_Percentage <= .30 && health_Percentage >= .50)
            {
                return 3;
            }
            else if (enemy_Health_Percentage <= .30 && health_Percentage < .50)
            {
                return 1;
            }

        }
        if(behavior == 2)
        {
            if(enemy_Health_Percentage >= .75 && health_Percentage >= .75)
            {
                return 4;
            }
            else if(enemy_Health_Percentage >= .75 && health_Percentage < .75 && health_Percentage >= .50)
            {
                return 0;
            }
            else if(enemy_Health_Percentage >= .75 && health_Percentage < .50)
            {
                return 0;
            }
            else if(enemy_Health_Percentage < .75 && enemy_Health_Percentage >= .45 && health_Percentage >= .75)
            {
                return 3;
            }

            else if(enemy_Health_Percentage < .75 && enemy_Health_Percentage >= .45 && health_Percentage < .75 && health_Percentage >= .50)
            {
                return 0;
            }
            else if(enemy_Health_Percentage <.45 && enemy_Health_Percentage > .15 && health_Percentage >= .75)
            {
                return 2;
            }
            else if (enemy_Health_Percentage <.45 && enemy_Health_Percentage > .15 && health_Percentage < .75 && health_Percentage >= .50)
            {
                return 0;
            }
            else if (enemy_Health_Percentage <=.15 && health_Percentage >= .50)
            {
                return 1;
            }
            else if (enemy_Health_Percentage <=.15 && health_Percentage < .50)
            {
                return 4;
            }
        }
        return 5;
    }

}
