using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class UnitDataClass : MonoBehaviour
{
    public int max_Health;
    public int current_Health;
    public float health_Percentage;

    class player : UnitDataClass
    {
        public player()
        {
            Debug.Log("Made a player class");
        }

    }

    class enemy : UnitDataClass
    {
        public enemy()
        {
            Debug.Log("Made an enemy class");
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

    public void CreateClasses()
    {
        enemy Enemy = new enemy();
        player Player = new player();
    }
}
