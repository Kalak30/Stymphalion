using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerHealthBoundaryTest : MonoBehaviour
{
    public int playerHealth;
    public double playerHealthPercentage;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    [Test]
    public void Test2()
    {
        playerHealth = Random.Range(-25, 150);
        playerHealthPercentage = playerHealth / 100.0;
        if(playerHealthPercentage > 1.0 || playerHealthPercentage < 0.001)
        {
            Debug.Log("Player health is outside of expected range, setting it to a correct value.");
            playerHealthPercentage = 1.0;
        }
        Debug.Log(playerHealth + "space" + playerHealthPercentage);
        if (playerHealthPercentage >= .75)
        {
            Debug.Log("If player health is " + playerHealth + " then I'll use a strong but slow attack");
        }
        else if (playerHealthPercentage < .75 && playerHealthPercentage >= .50)
        {
            Debug.Log("If player health is " + playerHealth + " then I'll use either a strong attack or a medium attack");
        }
        else if (playerHealthPercentage < .50 && playerHealthPercentage >= .25)
        {
            Debug.Log("If player health is " + playerHealth + " then I'll use a medium but faster attack");
        }
        else if (playerHealthPercentage < .25)
        {
            Debug.Log("If player health is " + playerHealth + " then I'll use the quickest attack that will defeat the player");
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
