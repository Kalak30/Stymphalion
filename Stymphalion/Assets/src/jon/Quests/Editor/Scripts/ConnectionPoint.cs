/*
 * Filename: ConnectionPoint.cs
 * Developer: Jon Kopf
 * Purpose: Give a representation of, and functionality to, a ConnectionPoint within a node of the quest editor
 */

using System;
using UnityEngine;


/// <summary>
/// All types of connection point
/// <list type="bullet">
///     <item>In</item>
///     <item>Out</item>
/// </list>
/// </summary>
public enum ConnectionPointType { In, Out }

/// <summary>
/// Representation of a connection point. Allows for drawing and selection of a point.
///  <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_rect</item>
///     <item>m_type</item>
///     <item>m_node</item>
///     <item>m_id</item>
///     <item>m_name</item>
///     <item>m_y</item>
///     <item>m_style</item>
///     <item>m_on_click_connection_point</item>
/// </list>
/// </summary>
public class ConnectionPoint
{
    public Rect m_rect;
    public ConnectionPointType m_type;
    public Node m_node;
    public int m_id;
    public string m_name;
    public float m_y;
    public GUIStyle m_style;
    public Action<ConnectionPoint> m_on_click_connection_point;

    /// <summary>
    ///
    /// </summary>
    /// <param name="node">The node this connection belongs to</param>
    /// <param name="id">Id of this connection point</param>
    /// <param name="name">The name of this connection point</param>
    /// <param name="y">The y position relative to parent node</param>
    /// <param name="type">Type of connection point</param>
    /// <param name="style">Graphical style of point</param>
    /// <param name="OnClickConnectionPoint">Action to take upon clicking on connection point</param>
    public ConnectionPoint(Node node, int id, string name, float y, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint)
    {
        m_id = id;
        m_node = node;
        m_name = name;
        m_y = y;
        m_type = type;
        m_style = style;
        m_on_click_connection_point = OnClickConnectionPoint;
        m_rect = new Rect(0, 0, 10f, 20f);
    }

    /// <summary>
    /// Draw a button for the connection point, and mark as selected if the button is pressed.
    /// </summary>
    public void Draw()
    {
        m_rect.y = m_y + m_node.m_rect.y + (m_node.m_rect.height * 0.5f) - m_rect.height * 0.5f;

        switch (m_type)
        {
            case ConnectionPointType.In:
                m_rect.x = m_node.m_rect.x - m_rect.width + 4f;
                break;

            case ConnectionPointType.Out:
                m_rect.x = m_node.m_rect.x + m_node.m_rect.width - 4f;
                break;
        }

        if (GUI.Button(m_rect, ""))
        {
            if (m_on_click_connection_point != null)
            {
                m_on_click_connection_point(this);
            }
        }
    }
}