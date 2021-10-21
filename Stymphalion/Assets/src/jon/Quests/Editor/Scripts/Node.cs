using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum NodeType
{
    Base,
    Quest,
    Step
}

[System.Serializable]
public class ConnectionRule
{
    public NodeType InputType;
    public int InputID;
    public NodeType OutputType;
    public int OutputID;

    public ConnectionRule(NodeType inputType, int inputID, NodeType outputType, int outputID)
    {
        InputType = inputType;
        InputID = inputID;
        OutputType = outputType;
        OutputID = outputID;
    }
}

[System.Serializable]
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
    public List<ConnectionPoint> m_inPoints;
    public List<ConnectionPoint> m_outPoints;
    public List<ConnectionRule> m_allowedInputs;
    public List<ConnectionRule> m_allowedOutputs;
    public NodeType m_type = NodeType.Base;
    /*    public ConnectionPoint m_inPoint;
        public ConnectionPoint m_outPoint;*/
    public Action<Node> m_onRemoveNode;

    public Node(Vector2 position, float width, float height, int ID, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
    {
        m_id = ID;
        m_rect = new Rect(position.x, position.y, width, height);
        m_style = nodeStyle;
        m_defaultNodeStyle = nodeStyle;
        m_selectedNodeStyle = selectedStyle;
        m_onRemoveNode = OnClickRemoveNode;
        m_title = "Window";
    }

    public void Drag(Vector2 delta)
    {
        m_rect.position += delta;
    }

    public void AddConnection(ConnectionPoint connectionPoint)
    {
        if (connectionPoint.m_type == ConnectionPointType.In && m_type == NodeType.Step)
        {
        }
    }

    public void DrawConnectionPoints()
    {
        if (m_inPoints != null)
        {
            foreach (ConnectionPoint p in m_inPoints)
            {
                p.Draw();
            }
        }

        if (m_outPoints != null)
        {
            foreach (ConnectionPoint p in m_outPoints)
            {
                p.Draw();
            }
        }
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

    public bool CanConnect(ConnectionPointType connType, int pointID, NodeType otherNodeType, ConnectionPointType otherConnType, int otherPointID)
    {
        bool canConnect = false;

        if (connType == ConnectionPointType.In)
        {
            foreach (var rule in m_allowedInputs)
            {
                if (rule.InputType == m_type && rule.InputID == pointID && rule.OutputType == otherNodeType && otherConnType == ConnectionPointType.Out && rule.OutputID == otherPointID)
                {
                    canConnect = true;
                    break;
                }
            }
        }
        else if (connType == ConnectionPointType.Out)
        {
            foreach (var rule in m_allowedOutputs)
            {
                if (rule.OutputType == m_type && rule.OutputID == pointID && rule.InputType == otherNodeType && otherConnType == ConnectionPointType.In && rule.InputID == otherPointID)
                {
                    canConnect = true;
                    break;
                }
            }
        }

        return canConnect;
    }
}