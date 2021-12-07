/*
 * Filename: NPCManager.cs
 * Developer: Jon Kopf
 * Purpose: Allow instantiation and control of npcs at runtime
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    private List<NPC> m_npcs;    
    private static NPCManager m_manager;
    public GameObject m_questnpc_prefab;
    public GameObject m_shopnpc_prefab;

    /// <summary>
    /// Returns the singleton 
    /// </summary>
    /// <returns></returns>
    public static NPCManager GetSingleton()
    {
        if (m_manager == null)
        {
            return GameObject.Find("Handlers").GetComponent<NPCManager>();
        }

        return m_manager;
    }

    private NPCManager() { }

    /// <summary>
    /// Create the initial NPCs 
    /// </summary>
    public void Start()
    {
        m_npcs = new List<NPC>();
        AddQuestNPC(1);
        AddShopNPC(null);
    }

    /// <summary>
    /// Everyframe, have the possibility of moving an npc by a random amount
    /// </summary>
    public void Update()
    {
        foreach (NPC npc in m_npcs)
        {
            int should_move = Random.Range(0, 100);
            if (!npc.m_moving && should_move > 85)
            {
                int rx_i = Random.Range(-10, 10);
                int ry_i = Random.Range(-10, 10);
                float rx_f = rx_i / 5;
                float ry_f = ry_i / 5;

                Vector2 curr_pos = npc.GetComponent<Transform>().position;
                Vector2 new_pos = new Vector2(curr_pos.x + rx_f, curr_pos.y + ry_f);
                npc.MoveTo(new_pos);
            }
        }
    }

    /// <summary>
    /// Adds a new quest npc with a specific quest
    /// </summary>
    /// <param name="quest_no"></param>
    public void AddQuestNPC(int quest_no)
    {
        
        GameObject npc_obj = GameObject.Instantiate(m_questnpc_prefab);
        QuestNPC npc = npc_obj.GetComponent<QuestNPC>();
        npc.transform.position = new Vector3(28,-4, 0);
        npc.m_npc_quest = QuestManager.GetQuestManager().GetQuest(quest_no);
        m_npcs.Add(npc);
    }

    /// <summary>
    /// Adds a new shop npc
    /// </summary>
    /// <param name="available_items"></param>
    public void AddShopNPC(List<Item.ItemType> available_items)
    {
        GameObject shop_obj = GameObject.Instantiate(m_shopnpc_prefab);
        ShopNPC npc = shop_obj.GetComponent<ShopNPC>();
        npc.transform.position = new Vector3(28, -6, 0);

        m_npcs.Add(npc);
    }
}
