using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerTests
{
    [UnityTest]
    public IEnumerator MovementTest()
    {
        SceneManager.LoadScene("HydraBattle");
        yield return new WaitForSeconds(2);

        var gameObject = new GameObject();
        var player = gameObject.AddComponent<CharacterMovement2D>();
        var movement = Input.GetAxis("Horizontal");

        Assert.IsTrue(movement != 0);
    }
}