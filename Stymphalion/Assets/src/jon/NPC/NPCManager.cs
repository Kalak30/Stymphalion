using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    private List<NPC> m_npcs;    
    private NPCManager m_manager;
    public GameObject m_questnpc_prefab;
    public GameObject m_shopnpc_prefab;
    


    public NPCManager GetSingleton()
    {
        if (m_manager == null)
        {
            return new NPCManager();
        }

        return m_manager;
    }

    public void Start()
    {
        m_npcs = new List<NPC>();
        AddQuestNPC(1);
    }

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

    public void AddQuestNPC(int quest_no)
    {
        
        GameObject npc_obj = GameObject.Instantiate(m_questnpc_prefab);
        QuestNPC npc = npc_obj.GetComponent<QuestNPC>();
        npc.m_npc_quest = QuestManager.GetQuestManager().GetQuest(quest_no);
        m_npcs.Add(npc);
    }
}
