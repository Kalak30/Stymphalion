using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string quest_name;
    public string quest_decription;
    public string quest_reward; // Needs to be an item from kyles stuff

    private string quest_status;
    private Quest_Step active_step;
    private List<Quest_Step> step_list;
}
