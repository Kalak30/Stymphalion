using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Test : MonoBehaviour
{
    private Quest_Manager qm;

    // Start is called before the first frame update
    private void Start()
    {
        qm = gameObject.AddComponent<Quest_Manager>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}