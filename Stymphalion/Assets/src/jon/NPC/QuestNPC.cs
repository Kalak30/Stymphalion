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
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Just some initial test code
    /// </summary>
    private void Awake()
    {
        m_npc_quest = new Quest("start", "the first quest", null);
        QuestManager.GetQuest_Manager().AddQuest(m_npc_quest);
    }


    private void Update()
    {
    }
}