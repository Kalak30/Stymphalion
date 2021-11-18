/*
 * Filename: BattleManager.cs
 * Developer: Riley Doyle
 * Purpose: To test at twhat speed the player can clip through a wall and break the Unity engine
 */


using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item></item>
/// </list>
/// </summary>
public class Wall_Test
{
    [UnityTest]
    ///<summary>  
    /// tests walls
    ///</summary> 
    ///<returns> IEnumerator </returns>
    public IEnumerator WallTest()
    {
        SceneManager.LoadScene("BossBattle");
        // wait for scene to load
        yield return new WaitForSeconds(2);
        GameObject player = GameObject.Find("Player");
        Player playerclass = player.GetComponent<Player>();
        //Character2DController controller = controller.GetComponent<Character2DController>();
        GameObject wall = GameObject.Find("Wall");

    }


}

