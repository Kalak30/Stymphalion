using System;
using UnityEditor;
using UnityEngine;

public class Node
{
    public int m_id;
    public Rect m_rect;
    public string m_title;
    public bool m_isDragged;
    public bool m_isSelected;
    public GUIStyle m_style;
    public GUIStyle m_defaultNodeStyle;
    public GUIStyle m_selectedNodeStyle;
    public ConnectionPoint m_inPoint;
    public ConnectionPoint m_outPoint;
    public Action<Node> m_onRemoveNode;

    public Node(Vector2 position, float width, float height, int ID, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
    {
        m_id = ID;
        m_rect = new Rect(position.x, position.y, width, height);
        m_style = nodeStyle;
        m_inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        m_outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        m_defaultNodeStyle = nodeStyle;
        m_selectedNodeStyle = selectedStyle;
        m_onRemoveNode = OnClickRemoveNode;
        m_title = "Window";
    }

    public void Drag(Vector2 delta)
    {
        m_rect.position += delta;
    }

    public void DrawConnectionPoints()
    {
        m_inPoint.Draw();
        m_outPoint.Draw();
    }

    public virtual void DrawWindow(int id)
    {
        GUI.Box(m_rect, m_title, m_style);
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (m_rect.Contains(e.mousePosition))
                    {
                        m_isDragged = true;
                        GUI.changed = true;
                        m_isSelected = true;
                        m_style = m_selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        m_isSelected = false;
                        m_style = m_defaultNodeStyle;
                    }
                }

                if (e.button == 1 && m_isSelected && m_rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                m_isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && m_isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove Node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        if (m_onRemoveNode != null)
        {
            m_onRemoveNode(this);
        }
    }
}