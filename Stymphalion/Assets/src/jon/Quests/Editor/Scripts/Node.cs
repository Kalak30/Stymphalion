/*
 * Filename: Node.cs
 * Developer: Jon Kopf
 * Purpose: Represent a node and allow for interactions on the window.
 */

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Different types of nodes
/// <list type="bullet">
///     <item>Base</item>
///     <item>Quest</item>
///     <item>Step</item>
/// </list>
/// </summary>
public enum NodeType
{
    Base,
    Quest,
    Step
}

/// <summary>
/// A rule for how different connection nodes can connect with each other.
/// </summary>
[Serializable]
public class ConnectionRule
{
    public int m_input_id;
    public NodeType m_input_type;
    public int m_output_id;
    public NodeType m_output_type;

    /// <summary>
    ///
    /// </summary>
    /// <param name="inputType">type of the input node</param>
    /// <param name="inputID">id of the input connection point</param>
    /// <param name="outputType">type of the output node</param>
    /// <param name="outputID">id of the output connection point</param>
    public ConnectionRule(NodeType inputType, int inputID, NodeType outputType, int outputID)
    {
        m_input_type = inputType;
        m_input_id = inputID;
        m_output_type = outputType;
        m_output_id = outputID;
    }
}

/// <summary>
/// Provides all information to uniquely characterize a node within the <see cref="QuestEditor"/>
/// ///  <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_id</item>
///     <item>m_rect</item>
///     <item>m_title</item>
///     <item>m_is_dragged</item>
///     <item>m_is_selected</item>
///     <item>m_style</item>
///     <item>m_default_node_style</item>
///     <item>m_selected_node_style</item>
///     <item>m_in_points</item>
///     <item>m_out_points</item>
///     <item>m_allowed_inputs</item>
///     <item>m_allowed_outputs</item>
///     <item>m_type</item>
///     <item>m_style</item>
///     <item>m_on_remove_node</item>
/// </list>
/// </summary>
[Serializable]
public class Node
{
    public List<ConnectionRule> m_allowed_inputs;
    public List<ConnectionRule> m_allowed_outputs;
    public GUIStyle m_default_node_style;
    public int m_id;
    public List<ConnectionPoint> m_in_points;
    public bool m_is_dragged;
    public bool m_is_selected;
    public Action<Node> m_on_remove_node;
    public List<ConnectionPoint> m_out_points;
    public Rect m_rect;
    public GUIStyle m_selected_node_style;
    public GUIStyle m_style;
    public string m_title;
    public NodeType m_type = NodeType.Base;

    /// <summary>
    ///
    /// </summary>
    /// <param name="position">position in the <see cref="QuestEditor"/> Editor window</param>
    /// <param name="width">Width of the new node</param>
    /// <param name="height">height of the new node</param>
    /// <param name="id"></param>
    /// <param name="node_style">Graphic style of the node</param>
    /// <param name="selected_style">Graphic style of a selected node</param>
    /// <param name="in_point_style">Graphic style of an in type <see cref="ConnectionPoint"/></param>
    /// <param name="out_point_style">Graphic style of an out type connection <see cref="ConnectionPoint"/></param>
    /// <param name="OnClickInPoint">Action to take when an in point is pressed </param>
    /// <param name="OnClickOutPoint">Action to take when an out point is pressed</param>
    /// <param name="OnClickRemoveNode">Action to take when remove node is pressed</param>
    public Node(Vector2 position, float width, float height, int id, GUIStyle node_style, GUIStyle selected_style, GUIStyle in_point_style, GUIStyle out_point_style, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
    {
        m_id = id;
        m_rect = new Rect(position.x, position.y, width, height);
        m_style = node_style;
        m_default_node_style = node_style;
        m_selected_node_style = selected_style;
        m_on_remove_node = OnClickRemoveNode;
        m_title = "Window";
    }

    /// <summary>
    /// Determines if a given connection is valid based on this <see cref="Node"/>'s connection rules
    /// </summary>
    /// <param name="conn_type">Type of connection on this node</param>
    /// <param name="point_id">Connection point id on this node</param>
    /// <param name="other_node_type">Type of the other node</param>
    /// <param name="other_conn_type">Type of connection on the other node</param>
    /// <param name="other_point_id">Connection point id on the other node</param>
    /// <returns></returns>
    public bool CanConnect(ConnectionPointType conn_type, int point_id, NodeType other_node_type, ConnectionPointType other_conn_type, int other_point_id)
    {
        bool canConnect = false;

        if (conn_type == ConnectionPointType.In)
        {
            foreach (var rule in m_allowed_inputs)
            {
                if (rule.m_input_type == m_type && rule.m_input_id == point_id && rule.m_output_type == other_node_type && other_conn_type == ConnectionPointType.Out && rule.m_output_id == other_point_id)
                {
                    canConnect = true;
                    break;
                }
            }
        }
        else if (conn_type == ConnectionPointType.Out)
        {
            foreach (var rule in m_allowed_outputs)
            {
                if (rule.m_output_type == m_type && rule.m_output_id == point_id && rule.m_input_type == other_node_type && other_conn_type == ConnectionPointType.In && rule.m_input_id == other_point_id)
                {
                    canConnect = true;
                    break;
                }
            }
        }

        return canConnect;
    }

    /// <summary>
    /// Moves the node when dragged
    /// </summary>
    /// <param name="delta">Change in position of the node</param>
    public void Drag(Vector2 delta)
    {
        m_rect.position += delta;
    }

    /// <summary>
    /// Draws the connection point buttons to the screen
    /// </summary>
    public void DrawConnectionPoints()
    {
        if (m_in_points != null)
        {
            foreach (ConnectionPoint p in m_in_points)
            {
                p.Draw();
            }
        }

        if (m_out_points != null)
        {
            foreach (ConnectionPoint p in m_out_points)
            {
                p.Draw();
            }
        }
    }

    /// <summary>
    /// Draws the <see cref="Node"/> as a <see cref="GUI.Box"/>
    /// </summary>
    /// <param name="id"></param>
    public virtual void DrawWindow(int id)
    {
        GUI.Box(m_rect, m_title, m_style);
    }

    /// <summary>
    /// Decides what to do when a certain event happens
    /// </summary>
    /// <param name="e">Event that happens</param>
    /// <returns>
    /// <list type="bullet">
    ///     <item><see langword="true"/> if mouse is dragged</item>
    ///     <item><see langword="false"/> otherwise</item>
    /// </list>
    /// </returns>
    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (m_rect.Contains(e.mousePosition))
                    {
                        m_is_dragged = true;
                        GUI.changed = true;
                        m_is_selected = true;
                        m_style = m_selected_node_style;
                    }
                    else
                    {
                        GUI.changed = true;
                        m_is_selected = false;
                        m_style = m_default_node_style;
                    }
                }

                if (e.button == 1 && m_is_selected && m_rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                m_is_dragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && m_is_dragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    /// <summary>
    /// Removes this node
    /// </summary>
    private void OnClickRemoveNode()
    {
        if (m_on_remove_node != null)
        {
            m_on_remove_node(this);
        }
    }

    /// <summary>
    /// Popup a context menu, allows for additional actions on a node
    /// </summary>
    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove Node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }
}