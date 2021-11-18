/*
 * Filename: Observer.cs
 * Developer: Riley Doyle
 * Purpose: listens for events initiated throughout the scene in order communicate information to the rest of the game
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item>hydraDeath</item>
/// </list>
/// </summary>
public class Observer : MonoBehaviour
{
    private int hydraDeath = 0;

    
    /// <summary>
    /// start function
    /// </summary>
    public void Start()
    {

    }


    /// <summary>
    /// OnEnable when hydra dies
    /// </summary>
    private void OnEnable()
    {
        Enemy.hydraDeath += hydraDead;
    }


    /// <summary>
    /// OnDisable when hydra dies
    /// </summary>
    private void OnDisable()
    {
        Enemy.hydraDeath -= hydraDead;
    }


    /// <summary>
    /// print to log
    /// </summary>
    private void hydraDead()
    {
        Debug.Log("Hydra Died");
    }
}