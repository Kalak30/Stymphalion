using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private int hydraDeath = 0;

    public void Start()
    {

    }

    private void OnEnable()
    {
        Enemy.hydraDeath += hydraDead;
    }

    private void OnDisable()
    {
        Enemy.hydraDeath -= hydraDead;
    }

    private void hydraDead()
    {
        Debug.Log("Hydra Died");
    }
}
