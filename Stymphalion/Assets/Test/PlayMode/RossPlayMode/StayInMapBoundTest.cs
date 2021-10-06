using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MapBoundTest
{
    [UnityTest]
    public IEnumerator MapNorthTest(){
        SceneManager.LoadScene("Main Island");
        // wiat for scene to load
        yield return new WaitForSeconds(2);

        PlayerClass player = GameObject.Find("Player").GetComponent<PlayerClass>();

        
       // player.player.velocity = new Vector2(0,5);
        
    }




}