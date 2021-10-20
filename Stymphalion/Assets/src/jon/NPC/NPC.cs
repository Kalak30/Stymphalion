/*
 * Filename: NPC.cs
 * Developer: Jon Kopf
 * Purpose:
 */

/*
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    public abstract void TouchingInteractable();

    private String name;
}