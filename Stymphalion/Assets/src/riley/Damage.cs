/*
 * Filename: Damage.cs
 * Developer: Riley Doyle
 * Purpose: will in the future comunicate with the observer object to initiate sound when the player attacks
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item>m_OnAnySwing</item>
/// </list>
/// </summary>
public class Damage : MonoBehaviour
{
    public static event Action m_OnAnySwing;

    public static event Action OnAnySwing;

    //trigers event when player attacks
    private void OnAttack()
    {
        if (Input.GetButtonDown("attack"))
        {
            OnAnySwing?.Invoke();
            m_OnAnySwing?.Invoke();
        }
    }
}
