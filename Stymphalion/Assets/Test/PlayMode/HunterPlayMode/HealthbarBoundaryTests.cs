/*
* Filename: HealthbarBoundaryTests.cs
* Developer: Hunter Leppek
* Purpose: This file performs boundary tests on the healthbar
*/
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;


/// <summary>
/// A class to test the boundaries of the healthbar
/// Member Variables
/// <list type = "bullet">
/// <item>m_max_health</item>
/// </list>
/// </summary>
public class HealthbarBoundaryTests 
{
    private int m_max_health = 100;

    /// <summary>
    /// A test function that tests the boundaries of the healthbar by increased and decreasing the health near the upper and lower bounds of the healthbar
    /// </summary>
    /// <returns>Asserts whether the healthbar stayed within bounds during all the tests</returns>
    [UnityTest]
    public IEnumerator HealthbarBoundary()
    {
        //Load scene with healthbar and find the player to which the healthbar is attached
        SceneManager.LoadScene("MainIsland");
        yield return new WaitForSeconds(3);
        PlayerClass mc = GameObject.Find("Player").GetComponent<PlayerClass>();

        //Starting player health is 100
        mc.m_health = m_max_health;

        //Lower boundary tests
        Debug.Log("Lower boundary tests:");
        //Below boundary (health = -5)
        mc.m_health -= 105;
        yield return null; //Let one frame pass so the healthbar script can check the player health before displaying it
        Assert.IsTrue(mc.m_health >= 0); //Report test result
        yield return new WaitForSeconds(2); //Wait 2 seconds so that we can see the healthbar in the scene
        Debug.Log("Current health: " + mc.m_health); //Print to console the current player health
        mc.m_health = m_max_health; //Reset health to the max so that the next test can begin

        //On boundary (health = 0)
        mc.m_health -= 100;
        yield return null;
        Assert.IsTrue(mc.m_health >= 0);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + mc.m_health);
        mc.m_health = m_max_health;

        //Above boundary (health = 5)
        mc.m_health -= 95;
        yield return null;
        Assert.IsTrue(mc.m_health >= 0);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + mc.m_health);
        mc.m_health = m_max_health;

        //Upper boundary tests
        Debug.Log("Upper boundary tests:");
        //Below boundary (health = 95)
        mc.m_health -= 5;
        yield return null;
        Assert.IsTrue(mc.m_health <= 100);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + mc.m_health);
        mc.m_health = m_max_health;

        //On boundary (health = 100)
        mc.m_health += 0;
        yield return null;
        Assert.IsTrue(mc.m_health <= 100);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + mc.m_health);
        mc.m_health = m_max_health;

        //Above boundary (health = 105)
        mc.m_health += 5;
        yield return null;
        Assert.IsTrue(mc.m_health <= 100);
        yield return new WaitForSeconds(2);
        Debug.Log("Current health: " + mc.m_health);
        mc.m_health = m_max_health;
        
    }
}
