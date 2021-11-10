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

    protected Animator m_animator;
    protected DialogueViewer m_dialogue_handler;
    protected String m_name;

    /// <summary>
    /// Gets called whenever the player interacts with this NPC
    /// </summary>
    public virtual void TouchingInteractable() { }

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_dialogue_handler = GameObject.Find("DialogueHandler").GetComponent<DialogueViewer>();
    }
}