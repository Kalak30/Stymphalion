/*
 *
 * Filename: EnemyAI.cs 
 * Developer: Trevor McGeary
 * Purpose: Determing which attack the enemy should use in combat
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// An attack has 3 values, speed, damage, and a bonus effect
    /// int damage
    /// int speed
    /// int bonus
    /// </summary>
    public class Attack
    {
       public int damage;
       public int speed;
       public int bonus;
       
    }

    //public variables
    public Attack strong_Attack = new Attack();
    public Attack fast_Attack = new Attack();
    public Attack mid_Attack = new Attack();
    public Attack bonus_Attack = new Attack();
    public Attack block = new Attack();

    //private variables
    private float player_Health_Percentage;


    // Start is called before the first frame update
    void Start()
    {
        //createAttackList();
        //player_Health = GetComponent<Player>
        //player_Health_Percentage = player_Health / 100;
        //GameObject.GetComponent<EnemyAttackList>.GetAttackValues(1);
        

    }

    // Update is called once per frame
    void Update()
    {
        //EnemyMove();
    }

    /// <summary>
    /// Creates the list of attacks an enemy can use in combat
    /// </summary>
    public void createAttackList()
    {
        //Attack strong_Attack = new Attack();
        strong_Attack.damage = Random.Range(69, 90);
        strong_Attack.speed = strong_Attack.damage / 10;


        //Attack fast_Attack = new Attack();
        fast_Attack.damage = Random.Range(15, 25);
        fast_Attack.speed = fast_Attack.damage / 10;

        // Attack mid_Attack = new Attack();
        mid_Attack.damage = Random.Range(35, 50);
        mid_Attack.speed = mid_Attack.damage / 10;

        // Attack bonus_Attack = new Attack();
        bonus_Attack.damage = Random.Range(30, 40);
        bonus_Attack.speed = bonus_Attack.damage / 10;
        bonus_Attack.bonus = Random.Range(1, 3);

        // Attack block = new Attack();
        block.damage = 0;
        block.speed = 5;
        block.bonus = 4;

    }

    /// <summary>
    /// Still in testing phase due to poor time management. Function returns the values of the attack the enemy will use.
    /// </summary>
    /// <returns></returns>
    public int EnemyMove()
    {
        int player_Health = Random.Range(1, 100);
        player_Health = 100;
        //int enemy_Behavior = Random.Range(1, 4);
        int enemy_Behavior = 1;

        player_Health_Percentage = player_Health / 100;
        //NEED TO GET PLAYER HEALTH
        

        if (enemy_Behavior == 1)
        {
            if (player_Health_Percentage >= .75)
            {
                Debug.Log(strong_Attack.damage);
                return strong_Attack.damage;
            }

            else if (player_Health_Percentage <.75 && player_Health_Percentage >= .50)
            {
                int check = Random.Range(1, 3);
                if (check > 1)
                {
                    Debug.Log(mid_Attack.damage);
                    return mid_Attack.damage;
                }
                else
                {
                    Debug.Log(bonus_Attack.damage);
                    return bonus_Attack.damage;
                }
                
            }

            else if (player_Health_Percentage <.50 && player_Health_Percentage >=.25)
            {
                Debug.Log(mid_Attack.damage);
                return mid_Attack.damage;
            }
            
            else if (player_Health_Percentage <.25)
            {
                Debug.Log(fast_Attack.damage);
                return fast_Attack.damage;
            }


        }

       
        return 0;
    }
}
