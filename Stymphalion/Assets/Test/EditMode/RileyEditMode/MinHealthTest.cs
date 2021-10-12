using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MinHealthTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void MinHealth_Test()
    {
        var player = new Player();
        int damage = 110;
        float expectedHealth = 0;

        player.TakeDamage(damage);
        Assert.IsTrue(player.GetCurrentHealth() == expectedHealth);

    }
    /*
    [Test]
    public void MaxHealthTest()
    {
        var player = new Player();
        int health = 50;
        float expectedHealth = player.GetMaxHealth();

        player.GainHealth(health);

        Assert.IsTrue(player.GetCurrentHealth() == expectedHealth);
    }
    */
}
