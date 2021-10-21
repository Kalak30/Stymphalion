/*
 * Filename: NPC.cs
 * Developer: Jon Kopf
 * Purpose: Gives the abstraction for an NPC type of object
 */

using System;
using UnityEngine;


/// <summary>
/// An abstract class for an NPC, inherts from MonoBehaviour and is able to be put into the game.
/// </summary>
public abstract class NPC : MonoBehaviour
{

    private String m_name;

    /// <summary>
    /// Gets called whenever the player interacts with this NPC
    /// </summary>
    public abstract void TouchingInteractable();
}