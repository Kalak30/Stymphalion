using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public static event Action OnAnySwing;

    private void OnAttack()
    {
        if (Input.GetButtonDown("attack"))
        {
            OnAnySwing?.Invoke();
        }
    }
}
