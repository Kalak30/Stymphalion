using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class HealthbarStressTest
{
    private int maxHealth = 100;
    
    [UnityTest]
    public IEnumerator HealthbarStress()
    {
        //Load scene with healthbar and find the player to which the healthbar is attached
        SceneManager.LoadScene("MainIsland");
        yield return new WaitForSeconds(3);
        PlayerClass MC = GameObject.Find("Player").GetComponent<PlayerClass>();

        //Starting player health is 100
        MC.m_health = maxHealth;

        int stressor = 100;
        //This should be a non-terminating loop, but stressor is growing infinitely large, so it is likely Unity will break with a large enough stressor
        while(MC.m_health <= 100 && MC.m_health >= 0)
        {
            MC.m_health += stressor;
            stressor *= 500;
            yield return null; //Let one frame pass so the healthbar script can check the player health before displaying it. In theory, the displayed health will be within [0, 100]
        }
        //If the displayed health is outside the bounds, then something must have been broken. 
        //In this case, Unity will crash.
        Debug.Log("Stressor: " + stressor);
        Debug.Log("Final health: " + MC.m_health);
        Assert.IsTrue(MC.m_health <= 100 && MC.m_health >= 0);


    }
}
