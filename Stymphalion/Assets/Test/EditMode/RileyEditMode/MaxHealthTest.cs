using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MaxHealthTest
{
    [Test]
    public void MaxHealth_Test()
    {
        var player = new Player();
        int health = 110;
        float expectedHealth = player.GetMaxHealth();

        player.GainHealth(health);

        Assert.IsTrue(player.GetCurrentHealth() == expectedHealth);
    }
}
