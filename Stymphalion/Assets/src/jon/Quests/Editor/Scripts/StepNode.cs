/*
 * Filename: StepNode.cs
 * Developer: Jon Kopf
 * Purpose: Provides all information to characterize a StepNode within the quest editor
 */

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// A subclass of <see cref="Node"/> that describes the a step within a quest
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_data</item>
/// </list>
/// </summary>
public class StepNode : Node
{
    public StepData m_data;

    /// <summary>
    ///
    /// </summary>
    /// <param name="position">xy position of new StepNode window</param>
    /// <param name="width">width of new StepNode window</param>
    /// <param name="height">height of new StepNode window</param>
    /// <param name="id">unique id of step node</param>
    /// <param name="node_style">Graphical style of node</param>
    /// <param name="selected_style">graphical style of a selected node</param>
    /// <param name="in_point_style">Graphical style of an in type of a Connection point</param>
    /// <param name="out_point_style">Graphical style of an out type of a Connection point</param>
    /// <param name="OnClickInPoint">Action to take when an in Connection point is clicked</param>
    /// <param name="OnClickOutPoint">Action to take when an out Connection point is clicked</param>
    /// <param name="OnClickRemoveNode">Action to take when remove node is clicked</param>
    public StepNode(Vector2 position, float width, float height, int id,
                    GUIStyle node_style, GUIStyle selected_style, GUIStyle in_point_style, GUIStyle out_point_style,
                    Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) :
            base(position, width, height, id, node_style, selected_style, in_point_style, out_point_style, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        m_type = NodeType.Step;
        m_title = "Step";

        m_in_points = new List<ConnectionPoint>
        {
            new ConnectionPoint(this, 0, "Quest", 7.5f, ConnectionPointType.In, in_point_style, OnClickInPoint),
            new ConnectionPoint(this, 1, "Previous", 35f, ConnectionPointType.In, in_point_style, OnClickInPoint)
        };

        m_out_points = new List<ConnectionPoint>
        {
            new ConnectionPoint(this, 0, "Quest", 7.5f, ConnectionPointType.Out, out_point_style, OnClickOutPoint),
            new ConnectionPoint(this, 1, "Next", 35f, ConnectionPointType.Out, out_point_style, OnClickOutPoint)
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

    /// <summary>
    /// Defines the layout of within the StepNode window
    /// </summary>
    /// <param name="id"></param>
    public override void DrawWindow(int id)
    {
        string step_name, step_description;

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Parent Quest");
        EditorGUILayout.LabelField("Step Name");
        EditorGUILayout.LabelField("Step Description");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("");
        step_name = EditorGUILayout.TextField(m_data.m_step_name);
        step_description = EditorGUILayout.TextField(m_data.m_step_description);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(50);

        EditorGUILayout.EndHorizontal();

        m_data.m_step_name = step_name;
        m_data.m_step_description = step_description;
    }
}