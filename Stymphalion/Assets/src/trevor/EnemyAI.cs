using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public class Attack
    {
       public int damage;
       public int speed;
       public int bonus;
    }


    private int player_Health;
    private float player_Health_Percentage;

    // Start is called before the first frame update
    void Start()
    {
        
        player_Health = Random.Range(1, 100);
        player_Health_Percentage = player_Health / 100;
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void EnemyMove()
    {
        Attack attack1 = new Attack();
        attack1.damage = Random.Range(69,90);
        attack1.speed = attack1.damage/10;
        attack1.bonus = 0;

        Attack attack2 = new Attack();
        attack2.damage = Random.Range(40,50);
        attack2.speed = attack2.damage/10;
        attack2.bonus = 0;

        Attack attack3 = new Attack();
        attack3.damage = Random.Range(15,25);
        attack3.speed = attack3.damage/10;
        attack3.bonus = 0;

        Attack attack4 = new Attack();
        attack4.damage = Random.Range(30,55);
        attack4.speed = attack4.damage/10;
        attack4.bonus = Random.Range(1,4);

        if(Input.GetKeyDown("/"))
        {
            Debug.Log("Attack 1 damage is " + attack1.damage + "with a speed of" + attack1.speed);
        }

        //For minimum viable product, a simple hypothetical method for determining what move the enemy should use, using only player health.
        if (player_Health_Percentage >= .75)
        {
            Debug.Log("If player health is " + player_Health + "then I'll use a strong but slow attack");
        }
        else if (player_Health_Percentage < .75 && player_Health_Percentage >= .50)
        {
            Debug.Log("If player health is " + player_Health + "then I'll use either a strong attack or a medium attack");
        }
        else if (player_Health_Percentage < .50 && player_Health_Percentage >= .25)
        {
            Debug.Log("If player health is " + player_Health + "then I'll use a medium but faster attack");
        }
        else if (player_Health_Percentage < .75)
        {
            Debug.Log("If player health is " + player_Health + "then I'll use the quickest attack that will defeat the player");
        }
    }
}
