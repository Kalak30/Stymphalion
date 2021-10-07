using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class HealthBoundTest
{
    // Start is called before the first frame update

    [UnityTest]
    public IEnumerator HealthUpperBoundTest(){
        SceneManager.LoadScene("MainIsland");

        yield return new WaitForSeconds(2);

        PlayerClass player = GameObject.Find("Player").GetComponent<PlayerClass>();

        player.health = 99;
        
        for(int i = 0; i < 3; i++){
            yield return null;
            Assert.IsTrue(player.health <= 100);
            player.health++;
            
        }
    }
    [UnityTest]
    public IEnumerator HealthLowerBoundTest(){
        SceneManager.LoadScene("MainIsland");

        yield return new WaitForSeconds(2);

        PlayerClass player = GameObject.Find("Player").GetComponent<PlayerClass>();

        player.health = 1;
        
        for(int i = 0; i < 3; i++){
            yield return null;
            Assert.IsTrue(player.health <= 100);
            player.health--;
            
        }

        
    }



    
}
