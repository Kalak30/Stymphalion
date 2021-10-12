using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class TrapTest
{
    [UnityTest]
    public IEnumerator TrapLocationTest(){
        SceneManager.LoadScene("MainIsland");
        // wiat for scene to load
        yield return new WaitForSeconds(2);

        GameObject player = GameObject.Find("Player");
        PlayerClass playerclass = player.GetComponent<PlayerClass>();
        playerclass.health = 100;
        GameObject trap = GameObject.Find("Trap");


        player.transform.position = trap.transform.position + new Vector3(0,2,0);
        while(playerclass.health == 100){
            player.transform.position = player.transform.position - new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.5f);
            //
        }
        Debug.Log("Player Position = " + player.transform.position);
        Debug.Log("Player Health = " + playerclass.health);
        Assert.IsTrue(playerclass.health < 100);

        


        
    }




}