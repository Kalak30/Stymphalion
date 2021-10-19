using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Quest_Editor : EditorWindow
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

    [MenuItem("QUESTS/Quest Editor")]
    public static void OpenWindow()
    {
        Quest_Editor wnd = GetWindow<Quest_Editor>();
        wnd.titleContent = new GUIContent("Quest Editor");
    }

    private void OnEnable()
    {
        m_nodeStyle = new GUIStyle();
        m_nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        m_nodeStyle.border = new RectOffset(12, 12, 12, 12);

        m_selectedNodeStyle = new GUIStyle();
        m_selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        m_selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        m_inPointStyle = new GUIStyle();
        m_inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        m_inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        m_inPointStyle.border = new RectOffset(4, 4, 12, 12);

        m_outPointStyle = new GUIStyle();
        m_outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        m_outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        m_outPointStyle.border = new RectOffset(4, 4, 12, 12);
    }

    /*public void CreateGUI()
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/src/jon/Quests/Editor/Resources/Edit_Window.uxml");
        VisualElement rootFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(rootFromUXML);
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/src/jon/Quests/Editor/Resources/Edit_Window.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        //Default icon?
    }*/

    // Node based system from https://gram.gs/gramlog/creating-node-based-editor-unity/
    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);

        DrawNodes();
        DrawConnections();

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        m_offset += m_drag * 0.5f;
        Vector3 newOffset = new Vector3(m_offset.x % gridSpacing, m_offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void DrawNodes()
    {
        BeginWindows();

        if (m_nodes != null)
        {
            foreach (Node n in m_nodes)
            {
                Quest_Node questNode = GetQuestNode(n.m_id);
                Step_Node stepNode = GetStepNode(n.m_id);

                if (questNode != null)
                {
                    n.m_rect = GUI.Window(n.m_id, n.m_rect, questNode.DrawWindow, n.m_title);
                }

                if (stepNode != null)
                {
                    n.m_rect = GUI.Window(n.m_id, n.m_rect, stepNode.DrawWindow, n.m_title);
                }

                n.DrawConnectionPoints();
            }
        }

        EndWindows();
    }

    private void DrawConnections()
    {
        if (m_connections != null)
        {
            foreach (Connection c in m_connections)
            {
                c.Draw();
            }
        }
    }

    private void ProcessEvents(Event e)
    {
        m_drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if (m_nodes != null)
        {
            // Start at end because last node is drawn at the top, so start there with updates
            for (int i = m_nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = m_nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Quest Node"), false, () => OnClickAddQuestNode(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Step Node"), false, () => OnClickAddStepNode(mousePosition));

        genericMenu.ShowAsContext();
    }

    private void OnClickAddQuestNode(Vector2 mousePosition)
    {
        if (m_nodes == null)
        {
            m_nodes = new List<Node>();
        }
        if (m_questNodes == null)
        {
            m_questNodes = new List<Quest_Node>();
        }

        Quest_Node n = new Quest_Node(mousePosition, 400, 150, m_nextNodeID, m_nodeStyle, m_selectedNodeStyle, m_inPointStyle, m_outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        n.Data = new Quest_NPC();
        m_nodes.Add(n);
        m_questNodes.Add(n);
        m_nextNodeID++;
    }

    private void OnClickAddStepNode(Vector2 mousePosition)
    {
        if (m_nodes == null)
        {
            m_nodes = new List<Node>();
        }
        if (m_stepNodes == null)
        {
            m_stepNodes = new List<Step_Node>();
        }

        Step_Node n = new Step_Node(mousePosition, 400, 100, m_nextNodeID, m_nodeStyle, m_selectedNodeStyle, m_inPointStyle, m_outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        n.Data = new Quest_Step("", "", null);
        m_nodes.Add(n);
        m_stepNodes.Add(n);
        m_nextNodeID++;
    }

    private void OnClickInPoint(ConnectionPoint inPoint)
    {
        m_selectedInPoint = inPoint;

        if (m_selectedOutPoint != null)
        {
            if (m_selectedOutPoint.m_node != m_selectedInPoint.m_node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickOutPoint(ConnectionPoint outPoint)
    {
        m_selectedOutPoint = outPoint;

        if (m_selectedInPoint != null)
        {
            if (m_selectedOutPoint.m_node != m_selectedInPoint.m_node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickRemoveConnection(Connection connection)
    {
        m_connections.Remove(connection);
    }

    private void OnClickRemoveNode(Node node)
    {
        if (m_connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            foreach (Connection c in m_connections)
            {
                if (c.m_inPoint == node.m_inPoint || c.m_outPoint == node.m_outPoint)
                {
                    connectionsToRemove.Add(c);
                }
            }

            foreach (Connection c in connectionsToRemove)
            {
                m_connections.Remove(c);
            }

            connectionsToRemove = null;
        }

        m_nodes.Remove(node);
    }

    private void CreateConnection()
    {
        if (m_connections == null)
        {
            m_connections = new List<Connection>();
        }

        m_connections.Add(new Connection(m_selectedInPoint, m_selectedOutPoint, OnClickRemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        m_selectedInPoint = null;
        m_selectedOutPoint = null;
    }

    private void OnDrag(Vector2 delta)
    {
        m_drag = delta;

        if (m_nodes != null)
        {
            foreach (Node node in m_nodes)
            {
                node.Drag(delta);
            }
        }

        GUI.changed = true;
    }

    private void DrawConnectionLine(Event e)
    {
        if (m_selectedInPoint != null && m_selectedOutPoint == null)
        {
            Handles.DrawBezier(
                m_selectedInPoint.m_rect.center,
                e.mousePosition,
                m_selectedInPoint.m_rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (m_selectedOutPoint != null && m_selectedInPoint == null)
        {
            Handles.DrawBezier(
                m_selectedOutPoint.m_rect.center,
                e.mousePosition,
                m_selectedOutPoint.m_rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    private Quest_Node GetQuestNode(int id)
    {
        if (m_questNodes is null) return null;
        foreach (var node in m_questNodes)
        {
            if (node.m_id == id)
            {
                return node;
            }
        }

        return null;
    }

    private Step_Node GetStepNode(int id)
    {
        if (m_stepNodes is null) return null;
        foreach (var node in m_stepNodes)
        {
            if (node.m_id == id)
            {
                return node;
            }
        }
        return null;
    }
}