/*
 * Filename: QuestNPC.cs
 * Developer: Jon Kopf
 * Purpose: Provide an interface to the QuestNPC and its quests
 */


/// <summary>
/// Provides an abstraction for a QuestNPC
/// </summary>
public class QuestNPC : NPC
{

    public Quest m_npc_quest;

    /// <summary>
    /// Gets called whenever the player interacts with a QuestNPC
    /// </summary>
    public override void TouchingInteractable()
    {
        m_animator.Play("Base Layer.QuestNPCTalkAnim", 0, 0);
        m_dialogue_handler.setDialogue(1);
    }

    private void Update()
    {
    }
}