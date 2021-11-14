/*
 * Filename: NPC.cs
 * Developer: Jon Kopf
 * Purpose: Gives the abstraction for an NPC type of object
 */

using System;
using UnityEngine;


/// <summary>
/// An abstract class for an NPC, inherits from MonoBehaviour and is able to be put into the game.
/// </summary>
public abstract class NPC : MonoBehaviour
{

    protected Animator m_animator;
    protected DialogueViewer m_dialogue_viewer;
    protected String m_name;
    public bool m_moving;
    public bool m_can_move;
    public Vector2 m_target;

    /// <summary>
    /// Gets called whenever the player interacts with this NPC
    /// </summary>
    public virtual void TouchingInteractable() { }

    
    /// <summary>
    /// Teleports the NPC to a given position. Possibility for different actions depending on the NPC
    /// </summary>
    /// <param name="pos"></param>
    public virtual void MoveTo(Vector2 pos)
    {

        GetComponent<Transform>().position = pos;
    }

    public virtual void MoveFrame()
    {

    }

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_dialogue_viewer = GameObject.Find("DialogueCanvas").GetComponent<DialogueViewer>();
        m_moving = false;
        m_can_move = true;
    }

    public void Update()
    {
        if (m_moving && m_can_move)
        {
            MoveFrame();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        StopMoving();

    }

    public void OnTriggerEnter(Collider other)
    {
        StopMoving();
    }

    protected void StopMoving()
    {
        m_moving = false;
        m_animator.SetBool("is_moving", false);
    }

    public virtual void StopTalking()
    {
        m_can_move = true;
        
    }
}