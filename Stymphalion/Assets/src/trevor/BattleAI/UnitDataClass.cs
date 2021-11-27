/*
 * Filename: UnitDataClass.cs
 * Developer: Trevor McGeary
 * Purpose: Contains the information for unit data.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for unit data. Contains health and behavior values
/// </summary>
public class UnitDataClass : MonoBehaviour
{
    public UnitDataClass[] test;
    public float max_Health;
    public float current_Health;
    public float health_Percentage;
    public int armor;
    public int behavior;
    public int enemy_Type;

    /// <summary>
    /// Sets values for a unit. 
    /// Virtual key word means a function is dynamically bound
    /// </summary>
    public virtual void setValues()
    {
        max_Health = 200;
        behavior = 1;
        armor = 0;
        current_Health = max_Health;
        enemy_Type = 0;
    }

    public class player : UnitDataClass
    {
        public player()
        {
            Debug.Log("Made a player class");
        }

    }

    public class enemy : UnitDataClass
    {
        public enemy()
        {
            Debug.Log("Made an enemy class");
        }

        public override void setValues()
        {
            max_Health = Random.Range(99, 301); 
            behavior = Random.Range(1, 3);
            armor = 0;
            current_Health = max_Health;
            enemy_Type = 1;
        }
    }

    public class weak_Enemy : enemy
    {
        public override void setValues()
        {
            max_Health = 50;
            behavior = 1;
            armor = 0;
            current_Health = max_Health;
            enemy_Type = 2;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
