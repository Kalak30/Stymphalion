using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Quest_Node : Node
{
    public Quest_NPC Data;

    public Quest_Node(Vector2 position, float width, float height, int ID, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) : base(position, width, height, ID, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        m_type = NodeType.Quest;
        m_title = "Quest";

        m_inPoints = new List<ConnectionPoint>
        {
            new ConnectionPoint(this, "Quest", 7.5f, ConnectionPointType.Out, outPointStyle, OnClickOutPoint),
            new ConnectionPoint(this, "Next", 35f, ConnectionPointType.Out, outPointStyle, OnClickOutPoint)
        };

        m_allowedInputs = new List<ConnectionRule>
        {
        };
        m_allowedOutputs = new List<ConnectionRule>
        {
            new ConnectionRule(NodeType.Step, 0, NodeType.Quest, 0)
        };
    }

    public override void DrawWindow(int id)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Quest Giver");
        EditorGUILayout.ObjectField(Data.npc_quest, typeof(Quest), false);
        EditorGUILayout.EndHorizontal();
    }
}