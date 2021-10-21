using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Quest_Node : Node
{
    public QuestData Data;

    public Quest_Node(Vector2 position, float width, float height, int ID, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) : base(position, width, height, ID, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        m_type = NodeType.Quest;
        m_title = "Quest";

        m_out_points = new List<ConnectionPoint>
        {
            new ConnectionPoint(this, 0,  "Quest", 7.5f, ConnectionPointType.Out, outPointStyle, OnClickOutPoint),
            new ConnectionPoint(this, 1, "Next", 35f, ConnectionPointType.Out, outPointStyle, OnClickOutPoint)
        };

        m_allowed_inputs = new List<ConnectionRule>
        {
        };
        m_allowed_outputs = new List<ConnectionRule>
        {
            new ConnectionRule(NodeType.Quest, 0, NodeType.Step, 0)
        };
    }

    private Item.ItemType itemType; // Cannot be local to DrawWindow. It will not allow for switching between types
    private Quest.QuestStatus questStatus;

    public override void DrawWindow(int id)
    {
        string questName;
        string questDescription;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        //EditorGUILayout.LabelField("Quest Giver");
        EditorGUILayout.LabelField("Quest Name");
        EditorGUILayout.LabelField("Quest Description");
        EditorGUILayout.LabelField("Quest Reward");
        EditorGUILayout.LabelField("Quest Status");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        //UnityEngine.Object questOwner = EditorGUILayout.ObjectField(Data, typeof(Quest_NPC), false);
        questName = EditorGUILayout.TextField(Data.m_quest_name);
        questDescription = EditorGUILayout.TextArea(Data.m_quest_description);
        itemType = (Item.ItemType)EditorGUILayout.EnumPopup(itemType);
        questStatus = (Quest.QuestStatus)EditorGUILayout.EnumPopup(questStatus);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);

        EditorGUILayout.EndHorizontal();

        Data.m_quest_name = questName;
        Data.m_quest_description = questDescription;
        Data.m_quest_reward = null; //itemType;
        Data.m_quest_status = (int)questStatus;
    }
}