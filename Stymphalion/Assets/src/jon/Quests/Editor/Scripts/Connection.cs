/*
 * Filename: Connection.cs
 * Developer: Jon Kopf
 * Purpose: Representation of connection line within quest editor
 */

using System;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Represents a connection line
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_in_point</item>
///     <item>m_out_point</item>
///     <item>m_on_click_remove_connection</item>
/// </list>
/// </summary>
public class Connection
{
    public ConnectionPoint m_in_point;
    public ConnectionPoint m_out_point;
    public Action<Connection> m_on_click_remove_connection;

    /// <summary>
    ///
    /// </summary>
    /// <param name="in_point">Start point of connection</param>
    /// <param name="out_point">End point of connection</param>
    /// <param name="OnClickRemoveConnection">Action to take upon removing a connection. </param>
    public Connection(ConnectionPoint in_point, ConnectionPoint out_point, Action<Connection> OnClickRemoveConnection)
    {
        m_in_point = in_point;
        m_out_point = out_point;
        m_on_click_remove_connection = OnClickRemoveConnection;
    }

    /// <summary>
    /// Draw a bezier curve from the start of connection to the end of a connection. Also allows functionality for
    /// removing a connection while it is currently drawn.
    /// </summary>
    public void Draw()
    {
        Handles.DrawBezier(
            m_in_point.m_rect.center,
            m_out_point.m_rect.center,
            m_in_point.m_rect.center + Vector2.left * 50f,
            m_out_point.m_rect.center - Vector2.left * 50f,
            Color.white,
            null,
            2f
        );

        if (GUI.Button(new Rect((m_in_point.m_rect.center + m_out_point.m_rect.center) * 0.5f, new Vector2(4, 8)), ""))
        //if (Handles.Button((m_inPoint.m_rect.center + m_outPoint.m_rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
        {
            if (m_on_click_remove_connection != null)
            {
                m_on_click_remove_connection(this);
            }
        }
    }
}