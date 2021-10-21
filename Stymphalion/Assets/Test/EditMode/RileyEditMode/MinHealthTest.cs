/*
 * Filename: MinHealthtest.cs
 * Developer: Riley Doyle
 * Purpose: To test the the player's health cannot go below the lower boundry
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
public class MinHealthTest
{
    [Test]
    ///<summary>  
    ///Tests player's lower health boundry 
    ///</summary> 
    ///<returns> void </returns>
    public void MinHealthTest()
    {
        var player = new Player();
        int damage = 200;
        float expectedHealth = 0;

        player.TakeDamage(damage);

        Assert.IsTrue(player.GetCurrentHealth() == expectedHealth);

    }


}

