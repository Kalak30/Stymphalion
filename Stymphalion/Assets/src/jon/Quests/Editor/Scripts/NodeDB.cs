using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDB : ScriptableObject
{
    private List<Node> m_nodes;
    private List<Quest_Node> m_questNodes;
    private List<Step_Node> m_stepNodes;
    private List<Connection> m_connections;
    private GUIStyle m_nodeStyle;
    private GUIStyle m_selectedNodeStyle;
    private GUIStyle m_inPointStyle;
    private GUIStyle m_outPointStyle;
    private ConnectionPoint m_selectedInPoint;
    private ConnectionPoint m_selectedOutPoint;
    private Vector2 m_drag;
    private Vector2 m_offset;
    private int m_nextNodeID;

    public Node GetNode(int id)
    {
        foreach (var node in m_nodes)
        {
            if (node.m_id == id)
            {
                return node;
            }
        }
        return null;
    }

    public void CreateNode(int type, Vector2 position)
    {
    }
}