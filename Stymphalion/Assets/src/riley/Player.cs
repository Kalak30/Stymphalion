using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        int currentHealth = maxHealth;

    }

    public void GainHealth(int health)
    {
        currentHealth += health;
        Debug.Log("After healing: " + currentHealth); 

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("After Damage: " + currentHealth);

        if (currentHealth <= 0)
        {
            int after = 0 - currentHealth;
            Debug.Log("Health is " + after + " below boundry");
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");

        //Make Enemy disappear
        GameObject objectToDisappear = GameObject.Find("Player");
        objectToDisappear.GetComponent<Renderer>().enabled = false;

        //Die animation

        //Disable the enemy
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}