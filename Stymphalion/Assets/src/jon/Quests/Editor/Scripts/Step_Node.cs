using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Step_Node : Node
{
    public Quest_Step Data;

    public Step_Node(Vector2 position, float width, float height, int ID, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) : base(position, width, height, ID, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        m_type = NodeType.Step;
        m_title = "Step";
    }

    public override void DrawWindow(int id)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent Quest");
        EditorGUILayout.ObjectField(Data.belongs_to, typeof(Quest), false);
        EditorGUILayout.EndHorizontal();
    }
}