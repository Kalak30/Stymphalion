/*
* Filename: MMBGM.cs
* Developer: Hunter Leppek
* Purpose: This file implements a stress test of the healthbar
*/
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;


/// <summary>
/// A class to stress test the healthbar
/// Member Variables
/// <list type = "bullet">
/// <item>m_max_health</item>
/// </list>
/// </summary>
public class HealthbarStressTest
{
    private int m_max_health = 100;
    
    /// <summary>
    /// A test function that incrementally stresses the healthbar until an exception occurs (such as Unity breaking)
    /// </summary>
    /// <returns>Returns the value of the stressor and the player health when an exception occurs</returns>
    [UnityTest]
    public IEnumerator HealthbarStress()
    {
        //Load scene with healthbar and find the player to which the healthbar is attached
        SceneManager.LoadScene("MainIsland");
        yield return new WaitForSeconds(3);
        PlayerClass mc = GameObject.Find("Player").GetComponent<PlayerClass>();

        //Starting player health is 100
        mc.m_health = m_max_health;

        int stressor = 100;
        //This should be a non-terminating loop, but stressor is growing infinitely large, so it is likely Unity will break with a large enough stressor
        while(mc.m_health <= 100 && mc.m_health >= 0)
        {
            mc.m_health += stressor;
            stressor *= 500;
            yield return null; //Let one frame pass so the healthbar script can check the player health before displaying it. In theory, the displayed health will be within [0, 100]
        }
        //If the displayed health is outside the bounds, then something must have been broken. 
        //In this case, Unity will crash.
        Debug.Log("Stressor: " + stressor);
        Debug.Log("Final health: " + mc.m_health);
        Assert.IsTrue(mc.m_health <= 100 && mc.m_health >= 0);


    }
}
