using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour {
    struct Quest_List{
        public List<Quest> quests;
        public Quest_List(){
            quests = new List<Quest>();
        }

        
        public void addQuest(Quest quest){
            quests.Add(quest);
        }
        
        public bool removeQuest(Quest quest){
            return quests.Remove(quest);
        }
        
        public Quest getQuest();

    }
  
    public void displayQuests(){
        Quest_List q; 
        q.addQuest();
        Debug.Log("=======================\n   ---- Quest_Manager ----\n displayQuests()\n =======================\n");
    }
}
