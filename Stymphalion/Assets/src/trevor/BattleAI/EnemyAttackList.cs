using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for attack data. An attack has a damage value, speed value, and a bonus value that determines if it has special properties.
/// </summary>
public class EnemyAttackList : MonoBehaviour
{
    //public EnemyAI enemyTest;

    public class Attack
    {
        public int damage;
        public int speed;
        public int bonus;
    
    }

    public Attack strong_Attack = new Attack();
    public Attack fast_Attack = new Attack();
    public Attack mid_Attack = new Attack();
    public Attack bonus_Attack = new Attack();
    public Attack block = new Attack();

    public int damage;
    public int speed;
    public int bonus;



    // Start is called before the first frame update
    void Start()
    {
        //createAttackList();
    }

    // Update is called once per frame
    void Update()
    {
       //GameObject.GetComponent<EnemyAI>.createAttackList();
    }

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
        bonus_Attack.damage = Random.Range(30,40);
        bonus_Attack.speed = bonus_Attack.damage / 10;
        bonus_Attack.bonus = Random.Range(1, 3);

       // Attack block = new Attack();
        block.damage = 0;
        block.speed = 5;
        block.bonus = 4;


    }

    public int GetAttackValues(int x)
    {
        if (x == 1)
        {
            return strong_Attack.damage;
        }
        else if (x == 2)
        {
            return fast_Attack.damage;
        }
        else if (x == 3)
        {
            return mid_Attack.damage;
        }
        else if (x==4)
        {
            return bonus_Attack.damage;
        }

        return 0;
    }
}
