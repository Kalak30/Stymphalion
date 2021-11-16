using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerFunctionTest
{
    PlayerClass player = PlayerClass.Instance;

    [SetUp]
    public void SetUp(){
        SceneManager.LoadScene("MainIsland");
    }

    [UnityTest]
    public IEnumerator Gold(){
        Item Gold = new Item { itemType = Item.ItemType.Gold, amount = 1 };
        player.AddToInventory(Gold);
        int gold = player.CountGold(Gold);
        yield return null;
        Assert.IsTrue(gold > 0); 
    }

    [UnityTest]
    public IEnumerator PlayerLocation(){
        player.SetPlayerLocation(5, 5);
        yield return null;
        Vector2 location = player.GetLocation();
        
        Assert.IsTrue(location == new Vector2(5, 5));
    }



}
