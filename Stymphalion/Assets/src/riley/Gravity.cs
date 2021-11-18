using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2(0,10f);

    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.gravity = new Vector2(0,10f);
    }
}

 