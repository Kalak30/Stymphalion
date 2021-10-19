using System;
using UnityEngine;

public enum ConnectionPointType { In, Out }

public class ConnectionPoint
{
    public Rect m_rect;
    public ConnectionPointType m_type;
    public Node m_node;
    public string m_name;
    public float m_y;
    public GUIStyle m_style;
    public Action<ConnectionPoint> m_onClickConnectionPoint;

    public ConnectionPoint(Node node, string name, float y, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint)
    {
        m_node = node;
        m_name = name;
        m_y = y;
        m_type = type;
        m_style = style;
        m_onClickConnectionPoint = OnClickConnectionPoint;
        m_rect = new Rect(0, 0, 10f, 20f);
    }

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
            if (m_onClickConnectionPoint != null)
            {
                m_onClickConnectionPoint(this);
            }
        }
    }
}