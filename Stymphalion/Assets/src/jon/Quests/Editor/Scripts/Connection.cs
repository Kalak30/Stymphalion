using System;
using UnityEditor;
using UnityEngine;

public class Connection
{
    public ConnectionPoint m_inPoint;
    public ConnectionPoint m_outPoint;
    public Action<Connection> m_onClickRemoveConnection;

    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
    {
        m_inPoint = inPoint;
        m_outPoint = outPoint;
        m_onClickRemoveConnection = OnClickRemoveConnection;
    }

    public void Draw()
    {
        Handles.DrawBezier(
            m_inPoint.m_rect.center,
            m_outPoint.m_rect.center,
            m_inPoint.m_rect.center + Vector2.left * 50f,
            m_outPoint.m_rect.center - Vector2.left * 50f,
            Color.white,
            null,
            2f
        );
        Debug.Log("This");
        if (GUI.Button(new Rect((m_inPoint.m_rect.center + m_outPoint.m_rect.center) * 0.5f, new Vector2(4, 8)), ""))
        //if (Handles.Button((m_inPoint.m_rect.center + m_outPoint.m_rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
        {
            if (m_onClickRemoveConnection != null)
            {
                m_onClickRemoveConnection(this);
            }
        }
    }
}