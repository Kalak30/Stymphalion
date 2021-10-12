using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt animation

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        Debug.Log("Enemy Died");

        //Make Enemy disappear
        GameObject objectToDisappear = GameObject.Find("Boss");
        objectToDisappear.GetComponent<Renderer>().enabled = false;

        //Die animation

        //Disable the enemy
      
    }
}