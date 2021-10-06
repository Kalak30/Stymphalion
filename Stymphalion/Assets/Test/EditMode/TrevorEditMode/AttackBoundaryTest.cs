using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AttackBoundaryTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Attack attack1 = new Attack();
    }
    public class Attack
    {
        public int damage;
        public int speed;
        public int bonus;
    }
    
    [Test]
    public void Test1()
    {
        Attack attack1 = new Attack();
        attack1.damage = Random.Range(69, 90);
        attack1.speed = attack1.damage / 10;
        attack1.bonus = 0;

        Attack attack2 = new Attack();
        attack2.damage = Random.Range(40, 50);
        attack2.speed = attack2.damage / 10;
        attack2.bonus = 0;

        Attack attack3 = new Attack();
        attack3.damage = Random.Range(15, 25);
        attack3.speed = attack3.damage / 10;
        attack3.bonus = 0;

        Attack attack4 = new Attack();
        attack4.damage = Random.Range(30, 55);
        attack4.speed = attack4.damage / 10;
        attack4.bonus = Random.Range(1, 4);

        Debug.Log("Attack 1 damage is " + attack1.damage + " with a speed of " + attack1.speed);
        Debug.Log("Attack 1 damage is " + attack2.damage + " with a speed of " + attack2.speed);
        Debug.Log("Attack 1 damage is " + attack3.damage + " with a speed of " + attack3.speed);
        Debug.Log("Attack 1 damage is " + attack4.damage + " with a speed of " + attack4.speed);


    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
