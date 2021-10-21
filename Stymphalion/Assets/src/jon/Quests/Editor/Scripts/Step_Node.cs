using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Step_Node : Node
{
    public StepData m_data;

    public Step_Node(Vector2 position, float width, float height, int ID, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) : base(position, width, height, ID, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        m_type = NodeType.Step;
        m_title = "Step";

        m_in_points = new List<ConnectionPoint>
        {
            new ConnectionPoint(this, 0, "Quest", 7.5f, ConnectionPointType.In, inPointStyle, OnClickInPoint),
            new ConnectionPoint(this, 1, "Previous", 35f, ConnectionPointType.In, inPointStyle, OnClickInPoint)
        };

        m_out_points = new List<ConnectionPoint>
        {
            new ConnectionPoint(this, 0, "Quest", 7.5f, ConnectionPointType.Out, outPointStyle, OnClickOutPoint),
            new ConnectionPoint(this, 1, "Next", 35f, ConnectionPointType.Out, outPointStyle, OnClickOutPoint)
        };

        m_allowed_inputs = new List<ConnectionRule>
        {
            new ConnectionRule(NodeType.Step, 0, NodeType.Step, 0),
            new ConnectionRule(NodeType.Step, 0, NodeType.Quest, 0),
            new ConnectionRule(NodeType.Step, 1, NodeType.Step, 1),
            new ConnectionRule(NodeType.Step, 1, NodeType.Quest, 1)
        };
        m_allowed_outputs = new List<ConnectionRule>
        {
            new ConnectionRule(NodeType.Step, 0, NodeType.Step, 0),
            new ConnectionRule(NodeType.Step, 0, NodeType.Step, 1)
        };
    }

    public override void DrawWindow(int id)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Parent Quest");
        EditorGUILayout.LabelField("Step Name");
        EditorGUILayout.LabelField("Step Description");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("");
        EditorGUILayout.TextField(m_data.m_step_name);
        EditorGUILayout.TextField(m_data.m_step_description);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(50);

        EditorGUILayout.EndHorizontal();
    }
}