/*
 * Filename: MaxHealthTest.cs
 * Developer: Riley Doyle
 * Purpose: To test that the player's health connot go above the upper boundry
 */


using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item></item>
/// </list>
/// </summary>
public class MaxHealthTest
{
    [Test]
    ///<summary>  
    ///Tests player's upper health boundry 
    ///</summary> 
    ///<returns> void </returns>
    public void MaxHealthTest()
    {
        var player = new Player();
        int health = 110;
        float expectedHealth = player.GetMaxHealth();

        player.GainHealth(health);

        Assert.IsTrue(player.GetCurrentHealth() == expectedHealth);
    }


}

