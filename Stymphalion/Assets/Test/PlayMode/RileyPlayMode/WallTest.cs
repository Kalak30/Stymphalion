using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class WallTest
{
    [UnityTest]
    public IEnumerator Wall_Test()
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