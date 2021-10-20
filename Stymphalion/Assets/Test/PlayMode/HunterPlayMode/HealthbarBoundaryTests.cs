using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;


public class HealthbarBoundaryTests 
{
    private int maxHealth = 100;
    [UnityTest]
    public IEnumerator HealthbarBoundary()
    {
        //Load scene with healthbar and find the player to which the healthbar is attached
        SceneManager.LoadScene("MainIsland");
        yield return new WaitForSeconds(3);
        PlayerClass MC = GameObject.Find("Player").GetComponent<PlayerClass>();

        //Starting player health is 100
        MC.m_health = maxHealth;

        //Lower boundary tests
        Debug.Log("Lower boundary tests:");
        //Below boundary (health = -5)
        MC.m_health -= 105;
        yield return null; //Let one frame pass so the healthbar script can check the player health before displaying it
        Assert.IsTrue(MC.m_health >= 0); //Report test result
        yield return new WaitForSeconds(2); //Wait 2 seconds so that we can see the healthbar in the scene
        Debug.Log("Current health: " + MC.m_health); //Print to console the current player health
        MC.m_health = maxHealth; //Reset health to the max so that the next test can begin

        //On boundary (health = 0)
        MC.m_health -= 100;
        yield return null;
        Assert.IsTrue(MC.m_health >= 0);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + MC.m_health);
        MC.m_health = maxHealth;

        //Above boundary (health = 5)
        MC.m_health -= 95;
        yield return null;
        Assert.IsTrue(MC.m_health >= 0);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + MC.m_health);
        MC.m_health = maxHealth;

        //Upper boundary tests
        Debug.Log("Upper boundary tests:");
        //Below boundary (health = 95)
        MC.m_health -= 5;
        yield return null;
        Assert.IsTrue(MC.m_health <= 100);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + MC.m_health);
        MC.m_health = maxHealth;

        //On boundary (health = 100)
        MC.m_health += 0;
        yield return null;
        Assert.IsTrue(MC.m_health <= 100);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + MC.m_health);
        MC.m_health = maxHealth;

        //Above boundary (health = 105)
        MC.m_health += 5;
        yield return null;
        Assert.IsTrue(MC.m_health <= 100);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + MC.m_health);
        MC.m_health = maxHealth;
        
    }
}
