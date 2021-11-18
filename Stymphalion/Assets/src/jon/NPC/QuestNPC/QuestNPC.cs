/*
 * Filename: QuestNPC.cs
 * Developer: Jon Kopf
 * Purpose: Provide an interface to the QuestNPC and its quests
 */


using UnityEngine;
/// <summary>
/// Provides an abstraction for a QuestNPC
/// </summary>
public class QuestNPC : NPC
{

    public Quest m_npc_quest;
    
    /// <summary>
    /// Sets the quest for this npc
    /// </summary>
    /// <param name="quest_no"></param>
    public QuestNPC(int quest_no)
    {
        m_npc_quest = QuestManager.GetQuestManager().GetQuest(quest_no);
    }

    /// <summary>
    /// Gets called whenever the player interacts with a QuestNPC
    /// </summary>
    public override void TouchingInteractable()
    {
        base.TouchingInteractable();
        m_animator.SetBool("is_talking", true);
        
        StopMoving();
        m_can_move = false;
        m_dialogue_viewer.SetDialogue(1, 1, this);
        
    }

    /// <summary>
    /// Sets moving to true and sets the target
    /// </summary>
    /// <param name="pos"></param>
    public override void MoveTo(Vector2 pos)
    {

        m_moving = true;
        m_animator.SetBool("is_moving", true);
        m_target = pos;
    }

    /// <summary>
    /// Moves to the target position on 30% ish of each time called. If next frame would be colliding with something,
    /// Dont move instead.
    /// </summary>
    public override void MoveFrame()
    {

        int r = Random.Range(0, 10);
        if (r > 7)
        {
            Vector2 temp_pos = Vector2.MoveTowards(GetComponent<Transform>().position, m_target, 0.01f);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(temp_pos, 0.0f);

            if (colliders.Length > 0)
            {
                StopMoving();
                return;
            }

            GetComponent<Transform>().position = temp_pos;

            if (Vector2.Distance(temp_pos, m_target) < 1)
            {
                StopMoving();
            }
        }
    }

    /// <summary>
    /// Called when the player stops interacting with the player.
    /// </summary>
    public override void StopTalking()
    {
        QuestManager.GetQuestManager().Next(m_npc_quest);
        base.StopTalking();
        m_animator.SetBool("is_talking", false);
    }
}