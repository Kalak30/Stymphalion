using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Start is called before the first frame update

    /// <summary>
    /// Do No Gravity In Over world
    /// </summary>
    void Start()
    {
        Physics.gravity = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
