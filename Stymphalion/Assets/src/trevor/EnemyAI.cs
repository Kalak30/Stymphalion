using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private int playerHealth;
    private float playerHealthPercentage;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = Random.Range(1, 100);
        playerHealthPercentage = playerHealth / 100;
        //For minimum viable product, a simple hypothetical method for determining what move the enemy should use, using only player health.
        if (playerHealthPercentage >= .75)
        {
            Debug.Log("If player health is " + playerHealth + "then I'll use a strong but slow attack");
        }
        else if (playerHealthPercentage < .75 && playerHealthPercentage >= .50)
        {
            Debug.Log("If player health is " + playerHealth + "then I'll use either a strong attack or a medium attack");
        }
        else if (playerHealthPercentage < .50 && playerHealthPercentage >= .25)
        {
            Debug.Log("If player health is " + playerHealth + "then I'll use a medium but faster attack");
        }
        else if (playerHealthPercentage < .75)
        {
            Debug.Log("If player health is " + playerHealth + "then I'll use the quickest attack that will defeat the player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
