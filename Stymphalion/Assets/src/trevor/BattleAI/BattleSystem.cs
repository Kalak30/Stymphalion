/*
 * Filename: BattleSystem.cs 
 * Developer: Trevor McGeary
 * Purpose: Runs the battle between the player and enemy AI
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// BattleSystem class. Controls how the battle takes place, creates
/// unit data, gets and assigns values to the Player and Enemy.
/// </summary>
public class BattleSystem : MonoBehaviour
{
    private static BattleSystem _instance;

    public static BattleSystem Instance {  get { return _instance; } }

    public DemoMode m_Demo;
    public PlayerAI playerData;
    public EnemyAIDemo enemyData;
    public UnitDataClass test;
    public UnitDataClass[] unitArray;
    public EnemyAttackList list_Instance;
    public EnemyAttackList[] player_Attack_Array;
    public EnemyAttackList[] enemy_Attack_Array;
    public int enemy_Number;
    private int player_Input;
    private int enemy_Input;
    

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    // Start is called before the first frame update
    /// <summary>
    /// Creates the enemy and player units and assigns them their respective moves. Player moves are preset, enemies are within 
    /// a certain range. Then, start the combat loop.
    /// </summary>
    void Start()
    {
        enemy_Number = Random.Range(1, 3);
        unitArray = new UnitDataClass[2];
        if (enemy_Number == 1)
        {
            unitArray[0] = gameObject.AddComponent<UnitDataClass.enemy>();
            unitArray[0].setValues();
        }
        else if (enemy_Number == 2)
        {
            unitArray[0] = gameObject.AddComponent<UnitDataClass.weak_Enemy>();
            unitArray[0].setValues();
        }
        enemyData.CreateEnemy();
        unitArray[1] = gameObject.AddComponent<UnitDataClass.player>();
        unitArray[1].setValues();
        //unitArray[2] = new UnitDataClass();
        //unitArray[2] = unitArray[1];
        playerData.max_Health = unitArray[1].max_Health;
        enemyData.max_Health = unitArray[0].max_Health;
        enemyData.enemy.max_Health = unitArray[0].max_Health;
        player_Attack_Array = new EnemyAttackList[5];
        player_Attack_Array[0] = gameObject.AddComponent<EnemyAttackList>();
        player_Attack_Array[0].damage = 0;
        player_Attack_Array[0].speed = 5;
        player_Attack_Array[0].bonus = 4;
        player_Attack_Array[1] = gameObject.AddComponent<EnemyAttackList>();
        player_Attack_Array[1].damage = 40;
        player_Attack_Array[1].speed = 3;
        player_Attack_Array[1].bonus = 0;
        player_Attack_Array[2] = gameObject.AddComponent<EnemyAttackList>();
        player_Attack_Array[2].damage = 70;
        player_Attack_Array[2].speed = 5;
        player_Attack_Array[2].bonus = 0;
        player_Attack_Array[3] = gameObject.AddComponent<EnemyAttackList>();
        player_Attack_Array[3].damage = 85;
        player_Attack_Array[3].speed = 8;
        player_Attack_Array[3].bonus = 0;
        player_Attack_Array[4] = gameObject.AddComponent<EnemyAttackList>();
        player_Attack_Array[4].damage = 35;
        player_Attack_Array[4].speed = 6;
        player_Attack_Array[4].bonus = 3;
        enemy_Attack_Array = new EnemyAttackList[5];
        enemy_Attack_Array[0] = gameObject.AddComponent<EnemyAttackList>();
        enemy_Attack_Array[0].damage = 0;
        enemy_Attack_Array[0].speed = 5;
        enemy_Attack_Array[0].bonus = 4;
        enemy_Attack_Array[1] = gameObject.AddComponent<EnemyAttackList>();
        enemy_Attack_Array[1].damage = Random.Range(15,25);
        enemy_Attack_Array[1].speed = enemy_Attack_Array[1].damage/10;
        enemy_Attack_Array[1].bonus = 0;
        enemy_Attack_Array[2] = gameObject.AddComponent<EnemyAttackList>();
        enemy_Attack_Array[2].damage = Random.Range(34,50);
        enemy_Attack_Array[2].speed = enemy_Attack_Array[2].damage/10;
        enemy_Attack_Array[2].bonus = 0;
        enemy_Attack_Array[3] = gameObject.AddComponent<EnemyAttackList>();
        enemy_Attack_Array[3].damage = Random.Range(69,90);
        enemy_Attack_Array[3].speed = enemy_Attack_Array[3].damage/10;
        enemy_Attack_Array[3].bonus = 0;
        Combat();
    }

    /// <summary>
    /// Main combat loop. Gets the player's input and the enemy's input, then assigns damage to the player and enemy.
    /// </summary>
    void Combat()
    {
        int x = 0;
        Debug.Log("Started combat");
        Debug.Log("Enemy is a type" + enemy_Number + "enemy");
        while (unitArray[0].current_Health > 0 && unitArray[1].current_Health > 0)
        {
            x++;
            player_Input = playerData.PlayerMove();
            enemy_Input = enemyData.EnemyMove();
            if (player_Input > 4)
            {
                Debug.Log("Something went wrong with player input");
                break;
            }
            if (enemy_Input > 4)
            {
                Debug.Log("Something went wrong with enemy input");
                break;
            }
            //enemy is faster
            if (enemy_Attack_Array[enemy_Input].speed < player_Attack_Array[player_Input].speed)
            {
                unitArray[1].current_Health = unitArray[1].current_Health - enemy_Attack_Array[enemy_Input].damage;
                Debug.Log("Enemy is faster, and attacks for " + enemy_Attack_Array[enemy_Input].damage);
                Debug.Log("Player has " + unitArray[1].current_Health + "remaining.");
                if (enemy_Attack_Array[enemy_Input].bonus == 4 && player_Attack_Array[player_Input].bonus != 3)
                {
                    unitArray[1].current_Health = unitArray[1].current_Health - player_Attack_Array[player_Input].damage / 2;
                    Debug.Log("Player attack was blocked and countered! Player takes " + player_Attack_Array[player_Input].damage / 2 + "damage, and has" + unitArray[1].current_Health + "health remaining.");
                }
                else if (unitArray[1].current_Health > 0 && enemy_Attack_Array[enemy_Input].bonus != 4)
                {
                    unitArray[0].current_Health = unitArray[0].current_Health - player_Attack_Array[player_Input].damage;
                    Debug.Log("Player is slower, and attacks for " + player_Attack_Array[player_Input].damage);  
                }
                else if (enemy_Attack_Array[enemy_Input].bonus == 4 && player_Attack_Array[player_Input].bonus == 3)
                {
                    unitArray[0].current_Health = unitArray[0].current_Health - player_Attack_Array[player_Input].damage;
                    Debug.Log("Player used a piercing attack, going through the block! Enemy takes " + player_Attack_Array[player_Input].damage + "damage.");
                }
                Debug.Log("Enemy has " + unitArray[0].current_Health + "remaining.");
            }
            //player is faster
            else if (enemy_Attack_Array[enemy_Input].speed > player_Attack_Array[player_Input].speed || enemy_Attack_Array[enemy_Input].speed == player_Attack_Array[player_Input].speed)
            {
                unitArray[0].current_Health = unitArray[0].current_Health - player_Attack_Array[player_Input].damage;
                enemyData.DamageCalc(player_Attack_Array[player_Input].damage);
                Debug.Log("Player is faster, and attacks for " + player_Attack_Array[player_Input].damage);
                Debug.Log("Enemy has " + unitArray[0].current_Health + " health remaining.");
                if (unitArray[0].current_Health > 0)
                {
                    unitArray[1].current_Health = unitArray[1].current_Health - enemy_Attack_Array[enemy_Input].damage;
                    if (enemy_Attack_Array[enemy_Input].bonus != 4)
                    {
                        Debug.Log("Enemy is slower, and attacks for " + enemy_Attack_Array[enemy_Input].damage);
                    }
                    else
                        Debug.Log("Enemy couldn't get it's guard up in time, and fails to block.");;
                }
                if (player_Attack_Array[player_Input].bonus == 4 && enemy_Attack_Array[enemy_Input].bonus != 3)
                {
                    unitArray[0].current_Health = unitArray[0].current_Health - enemy_Attack_Array[enemy_Input].damage / 2;
                    enemyData.DamageCalc(enemy_Attack_Array[enemy_Input].damage / 2);
                    Debug.Log("Enemy attack was blocked and countered! Enemy takes " + enemy_Attack_Array[enemy_Input].damage / 2 + "damage, and has" + unitArray[0].current_Health + "health remaining.");
                }
                else if (player_Attack_Array[player_Input].bonus == 4 && enemy_Attack_Array[enemy_Input].bonus == 3)
                {
                    unitArray[1].current_Health = unitArray[1].current_Health - enemy_Attack_Array[enemy_Input].damage;
                    Debug.Log("Enemy used a piercing attack, going through the block! Player takes " + enemy_Attack_Array[enemy_Input].damage + "damage.");
                }
                Debug.Log("Player has " + unitArray[1].current_Health + " health remaining.");
            }
           playerData.current_Health = unitArray[1].current_Health;
           unitArray[0].health_Percentage = unitArray[0].current_Health / unitArray[0].max_Health;
           unitArray[1].health_Percentage = unitArray[1].current_Health / unitArray[1].max_Health;
           playerData.enemy_Health_Percentage = unitArray[0].health_Percentage;
           playerData.health_Percentage = unitArray[1].health_Percentage;
           enemyData.current_Health = unitArray[0].current_Health;
           enemyData.player_Health_Percentage = unitArray[1].health_Percentage;
           enemyData.health_Percentage = unitArray[0].health_Percentage;
        }
        if (unitArray[0].current_Health > 0)
        {
            Debug.Log("Enemy won");
        }
        else if (unitArray[1].current_Health > 1)
        {
            Debug.Log("Player won");
            //SceneManager.LoadScene("MainIsland");
            //m_Demo.testClass.StartReplay();
        }
    }
}
